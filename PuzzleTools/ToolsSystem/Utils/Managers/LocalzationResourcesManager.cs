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

        public string Description_AdvanceBulkRenameTool { get { return resourceMap.GetValue(ConstStrings.DescriptionAdvanceBulkRenameTool, resourceContext).ValueAsString; } }

        public string Description_BulkRenameTool { get { return resourceMap.GetValue(ConstStrings.DescriptionBulkRenameTool, resourceContext).ValueAsString; } }

        public string Description_FileInformationBrower { get { return resourceMap.GetValue(ConstStrings.DescriptionFileInformationBrower, resourceContext).ValueAsString; } }

        public string Description_FileInformationEditor { get { return resourceMap.GetValue(ConstStrings.DescriptionFileInformationEditor, resourceContext).ValueAsString; } }

        public string MenuItemsFileTools { get { return resourceMap.GetValue(ConstStrings.MenuItemsFileTools, resourceContext).ValueAsString; } }

        public string MenuItemsFolderTools { get { return resourceMap.GetValue(ConstStrings.MenuItemsFolderTools, resourceContext).ValueAsString; } }

        public string Tool_AdvanceBulkRenameTool { get { return resourceMap.GetValue(ConstStrings.ToolAdvanceBulkRenameTool, resourceContext).ValueAsString; } }

        public string Tool_BulkRenameTool { get { return resourceMap.GetValue(ConstStrings.ToolBulkRenameTool, resourceContext).ValueAsString; } }

        public string Tool_FileInformationBrower { get { return resourceMap.GetValue(ConstStrings.ToolFileInformationBrower, resourceContext).ValueAsString; } }

        public string Tool_FileInformationEditor { get { return resourceMap.GetValue(ConstStrings.ToolFileInformationEditor, resourceContext).ValueAsString; } }

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
