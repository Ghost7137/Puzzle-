using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace ToolsSystem
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        #region 成员

        public string _viewHeader;

        private List<NavigationViewItem> _itemList;

        #endregion

        #region 事件

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region 属性

        public string ViewHeader
        {
            get { return _viewHeader; }
            set
            {
                if(_viewHeader != value)
                {
                    _viewHeader = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ViewHeader"));
                }
            }
        }

        public List<NavigationViewItem> MenuItemsSource { get { return _itemList; } }

        #endregion

        #region 构造函数

        public MainPage()
        {
            this.InitializeComponent();
        }

        #endregion

        #region 方法

        public void InstallPage()
        {
            InstallItems();
        }

        private void InstallItems()
        {
            _itemList = new List<NavigationViewItem>();
            _itemList.Add(new NavigationViewItem() { Content = LocalzationResourcesManager.Instance.MenuItemsFileTools, Icon = new FontIcon() { Glyph = ConstStrings.GlyphFile }, Tag = FrameTypes.FileToolsBrowserFrame, IsSelected = true });
            _itemList.Add(new NavigationViewItem() { Content = LocalzationResourcesManager.Instance.MenuItemsFolderTools, Icon = new FontIcon() { Glyph = ConstStrings.GlyphFolder }, Tag = FrameTypes.FolderToolsBrowserFrame });
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(ConstStrings.MenuItemsSource));
        }

        #endregion

        private void OnSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if(args.IsSettingsSelected)
            {

            }
            else
            {
                FrameTypes pageType = (FrameTypes)((args.SelectedItem as NavigationViewItem).Tag);
                ViewHeader = (string)((args.SelectedItem as NavigationViewItem).Content);
                switch (pageType)
                {
                    case FrameTypes.FileToolsBrowserFrame:

                        break;
                    case FrameTypes.FolderToolsBrowserFrame:

                        break;
                    default:

                        break;
                }
            }
        }
    }
}
