using Cartesian.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using CartesianViewerModule;
using TopNavigatorModule;
using LoadDataModule;

namespace Cartesian
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        /// <summary>
        /// Introducing Modules
        /// </summary>
        /// <param name="moduleCatalog"></param>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<TopNavigatorModuleConfig>();
            moduleCatalog.AddModule<CartesianViewerMaduleConfig>();
            moduleCatalog.AddModule<LoadDataModuleConfig>();
        }

    }
}
