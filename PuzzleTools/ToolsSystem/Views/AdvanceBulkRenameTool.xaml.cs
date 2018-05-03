using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
    public sealed partial class AdvanceBulkRenameTool : Page
    {
        AdvanceBulkRenameToolViewModel _viewModel;

        AdvanceInfo _currentInfo;

        public AdvanceBulkRenameTool()
        {
            this.InitializeComponent();
            _viewModel = new AdvanceBulkRenameToolViewModel();
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

        private void OnListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_currentInfo != null)
                _currentInfo = null;
            _currentInfo = new AdvanceInfo();
            EditText.Text = (FileList.SelectedItem as AdvanceRenameModel).OriginalName;
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            if (_currentInfo != null)
            {
                _viewModel.Information.Add(_currentInfo);
                _viewModel.UpdateRename();
                if (_currentInfo != null)
                    _currentInfo = null;
                EditText.Text = string.Empty;
                HandleText.Text = string.Empty;
                LanguageSelector.SelectedIndex = -1;
                Capitalize.IsChecked = false;
            }
        }

        private void OnEditTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_currentInfo != null)
                _currentInfo.EditName = (sender as TextBox).Text;
        }

        private void OnHandleTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_currentInfo != null)
                _currentInfo.HandleName = (sender as TextBox).Text;
        }

        private void OnCapitalizeCheckBoxClick(object sender, RoutedEventArgs e)
        {
            if (_currentInfo != null)
                _currentInfo.IsCapitalize = (sender as CheckBox).IsChecked.Value;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_currentInfo != null)
                _currentInfo.Language = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
        }
    }

    public class AdvanceBulkRenameToolViewModel : INotifyPropertyChanged
    {
        ObservableCollection<AdvanceRenameModel> _fileSource;
        ObservableCollection<AdvanceInfo> _informationSource;

        public event PropertyChangedEventHandler PropertyChanged;

        public void LoadedFiles(IReadOnlyList<StorageFile> files)
        {
            if (_fileSource.Count > 0)
                _fileSource.Clear();
            foreach (StorageFile file in files)
            {
                AdvanceRenameModel fileItem = new AdvanceRenameModel() { File = file, OriginalName = file.Name, Rename = file.Name };
                _fileSource.Add(fileItem);
            }
        }

        void RaisedPropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        string GetFormat(string name)
        {
            int index = name.LastIndexOf('.');
            string format = name.Remove(0, index);
            return format;
        }

        public void UpdateRename()
        {
            foreach (AdvanceRenameModel item in _fileSource)
            {
                string rename = CreateNewName(item.Rename);
                item.Rename = rename;
            }
        }

        string CreateNewName(string originalName)
        {
            string tempName = originalName;
            foreach(AdvanceInfo info in _informationSource)
            {
                if(info.IsCapitalize)
                {
                    if (string.IsNullOrEmpty(info.HandleName))
                    {
                        string editName = Capitalize(info.EditName, info.Language);
                        tempName = originalName.Replace(info.EditName, editName);
                    }
                    else
                    {
                        info.HandleName = Capitalize(info.HandleName, info.Language);
                        tempName = originalName.Replace(info.EditName, info.HandleName);
                    }
                }
                else
                    tempName = originalName.Replace(info.EditName, info.HandleName);
            }
            return tempName;
        }

        string Capitalize(string text,string language)
        {
            CultureInfo cultureInfo = new CultureInfo(language);
            string lower = cultureInfo.TextInfo.ToLower(text);
            return cultureInfo.TextInfo.ToTitleCase(lower);
        }

        public async void SaveFilesAsync()
        {
            StorageFolder folder = await KnownFolders.VideosLibrary.CreateFolderAsync("Temp", CreationCollisionOption.OpenIfExists);
            foreach (AdvanceRenameModel item in _fileSource)
            {
                if (item.OriginalName != item.Rename)
                    await item.File.MoveAsync(folder, item.Rename);
            }
        }

        public ObservableCollection<AdvanceInfo> Information { get { return _informationSource; } }
        public ObservableCollection<AdvanceRenameModel> FilesSource { get { return _fileSource; } }

        public AdvanceBulkRenameToolViewModel()
        {
            _fileSource = new ObservableCollection<AdvanceRenameModel>();
            _informationSource = new ObservableCollection<AdvanceInfo>();
        }
    }

    public class AdvanceRenameModel
    {
        public StorageFile File { get; set; }
        public string OriginalName { get; set; }
        public string Rename { get; set; }
    }

    public class AdvanceInfo
    {
        public string EditName { get; set; }
        public string HandleName { get; set; }
        public string Language { get; set; }
        public bool IsCapitalize { get; set; }
    }

    public enum AdvanceType
    {
        Capitalize
    }
}
