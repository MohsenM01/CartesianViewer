using CartesianViewerModule.ViewModels;
using Prism.Events;
using Prism.Ioc;

namespace CartesianViewerModule.Views
{
    /// <summary>
    /// Interaction logic for CartesianUserControl.xaml
    /// </summary>
    public partial class CartesianViewerUserControl 
    {
        /// <summary>
        /// 
        /// </summary>
        public CartesianViewerUserControl()
        {
            InitializeComponent();

            //Need to detect Grid Size
            var eventAggregator = ContainerLocator.Container.Resolve<IEventAggregator>();
           DataContext = new CartesianViewerUserControlViewModel(MainGrid, eventAggregator);
        }

    }
}
