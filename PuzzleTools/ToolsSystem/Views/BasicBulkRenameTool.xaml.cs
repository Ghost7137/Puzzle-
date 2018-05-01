using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace ToolsSystem
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BasicBulkRenameTool : Page
    {
        BasicBulkRenameToolViewModel _viewModel;

        public BasicBulkRenameTool()
        {
            this.InitializeComponent();
            _viewModel = new BasicBulkRenameToolViewModel();
            DataContext = _viewModel;
        }

        private async void OnFolderPickerClick(object sender, RoutedEventArgs e)
        {
            var picker = new FolderPicker();
            picker.FileTypeFilter.Add(".");
            picker.SuggestedStartLocation = PickerLocationId.Desktop;
            var folder = await picker.PickSingleFolderAsync();
            IReadOnlyList<StorageFile> files = await folder.GetFilesAsync();
            _viewModel.LoadedFiles(files);
        }

        private void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveFilesAsync();
        }

        private void OnNumberClick(object sender, RoutedEventArgs e)
        {
            _viewModel.UpdateRename();
        }

        private void OnCharacterClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnDateTimeClick(object sender, RoutedEventArgs e)
        {

        }
    }

    public class BasicBulkRenameToolViewModel : INotifyPropertyChanged
    {
        ObservableCollection<RenameFileItem> _fileSource;

        public void LoadedFiles(IReadOnlyList<StorageFile> files)
        {
            if (_fileSource.Count > 0)
                _fileSource.Clear();
            foreach(StorageFile file in files)
            {
                RenameFileItem fileItem = new RenameFileItem() { File = file, OriginalName = file.Name };
                _fileSource.Add(fileItem);
            }
        }

        public void UpdateRename()
        {
            int start = 1;
            int max = _fileSource.Count;
            foreach (RenameFileItem item in _fileSource)
            {
                string rename = CreateNewName(start, max) + GetFormat(item.OriginalName);
                item.Rename = rename;
                start++;
            }
        }

        string CreateNewName(int start,int max)
        {
            string current = start.ToString();
            int maxLength = max.ToString().Length;
            if(maxLength > current.Length)
            {
                int zeroCount = maxLength - current.Length;
                for (int i = 0; i < zeroCount; i++)
                    current = "0" + current;
            }
            return current;
        }

        string GetFormat(string name)
        {
            int index = name.LastIndexOf('.');
            string format = name.Remove(0, index);
            return format;
        }

        public async void SaveFilesAsync()
        {
            foreach(RenameFileItem item in _fileSource)
            {
                await item.File.RenameAsync(item.Rename);
            }
        }

        public ObservableCollection<RenameFileItem> FilesSource { get { return _fileSource; } }

        public event PropertyChangedEventHandler PropertyChanged;

        void RaisedPropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        public BasicBulkRenameToolViewModel() { _fileSource = new ObservableCollection<RenameFileItem>(); }

    }

    public class RenameFileItem
    {
        public StorageFile File { get; set; }
        public string OriginalName { get; set; }
        public string Rename { get; set; }
    }

    public enum RenameType
    {
        Number
    }
}
