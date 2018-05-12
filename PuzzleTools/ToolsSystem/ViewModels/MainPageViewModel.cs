using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ToolsSystem
{
    public class MainPageViewModel : ViewModelBase
    {
        #region 成员

        private object _selectedMenuItem;

        public SystemCommand<NavigationView> _navigate;

        private List<NavigationViewItem> _itemList;

        #endregion

        #region 属性

        public object SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { SetValue(_selectedMenuItem, value, ConstStrings.SelectedMenuItem); }
        }

        public SystemCommand<NavigationView> NavigateCommand { get { return _navigate; } }

        public List<NavigationViewItem> MenuItemsSource { get { return _itemList; } }

        #endregion

        #region 方法

        private void InstallItems()
        {
            _itemList = new List<NavigationViewItem>();
            _itemList.Add(new NavigationViewItem() { Content = LocalzationResourcesManager.Instance.MenuItemsFileTools, Icon = new FontIcon() { Glyph = ConstStrings.GlyphFile } });
            _itemList.Add(new NavigationViewItem() { Content = LocalzationResourcesManager.Instance.MenuItemsFolderTools, Icon = new FontIcon() { Glyph = ConstStrings.GlyphFolder } });
        }

        private void InstallCommands()
        {
            _navigate = new SystemCommand<NavigationView>(new Action<NavigationView>(FrameNavigation));
        }

        void FrameNavigation(NavigationView view)
        {

        }

        #endregion

        #region 构造函数

        public MainPageViewModel()
        {
            InstallItems();
        }

        #endregion
    }
}
