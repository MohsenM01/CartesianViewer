using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using TopNavigatorModule.Views;

namespace TopNavigatorModule
{
    /// <summary>
    /// 
    /// </summary>
    public class TopNavigatorModuleConfig : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerProvider"></param>
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("TopNavigator", typeof(TopNavigatorUserControl));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}