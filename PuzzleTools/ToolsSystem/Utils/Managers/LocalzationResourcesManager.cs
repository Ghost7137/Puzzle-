using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;

namespace ToolsSystem
{
    public class LocalzationResourcesManager
    {
        #region 成员

        private static readonly LocalzationResourcesManager _instance = new LocalzationResourcesManager();
        private static ResourceContext resourceContext;
        private static ResourceMap resourceMap;

        #endregion

        #region 属性

        public static LocalzationResourcesManager Instance { get { return _instance; } }

        public string MenuItemsFileTools { get { return resourceMap.GetValue(ConstStrings.MenuItemsFileTools, resourceContext).ValueAsString; } }

        public string MenuItemsFolderTools { get { return resourceMap.GetValue(ConstStrings.MenuItemsFolderTools, resourceContext).ValueAsString; } }

        #endregion

        #region 构造函数

        static LocalzationResourcesManager()
        {
            resourceContext = ResourceContext.GetForCurrentView();
            resourceMap = ResourceManager.Current.MainResourceMap.GetSubtree(ConstStrings.ResourceNamespace);
        }

        #endregion

    }
}
