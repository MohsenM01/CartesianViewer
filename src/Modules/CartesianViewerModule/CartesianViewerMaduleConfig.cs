using CartesianViewerModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace CartesianViewerModule
{
    /// <summary>
    /// 
    /// </summary>
    public class CartesianViewerMaduleConfig : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerProvider"></param>
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("CartesianViewer", typeof(CartesianViewerUserControl));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CartesianViewerUserControl>("CartesianViewer");
        }
    }
}