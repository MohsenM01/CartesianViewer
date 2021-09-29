using LoadDataModule.ViewModels;
using LoadDataModule.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace LoadDataModule
{
    /// <summary>
    /// 
    /// </summary>
    public class LoadDataModuleConfig : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerProvider"></param>
        public void OnInitialized(IContainerProvider containerProvider)
        {
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<FileViewerUserControl, FileViewerUserControlViewModel>("LoadDataDialog");
        }
    }
}