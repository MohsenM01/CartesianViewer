using CartesianViewerModule.Services;
using Common.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Controls;
using CartesianViewerModule.Shapes;

namespace CartesianViewerModule.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class CartesianViewerUserControlViewModel : BindableBase
    {

        private readonly Grid _mainViewBox;

        private bool _redrawCanvasWhenSizeChange;
        private double _scale;

        /// <summary>
        /// 
        /// </summary>
        public bool RedrawCanvasWhenSizeChange
        {
            get => _redrawCanvasWhenSizeChange;
            set => SetProperty(ref _redrawCanvasWhenSizeChange, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public double Scale
        {
            get => _scale;
            set => SetProperty(ref _scale, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainViewBox"></param>
        /// <param name="eventAggregator"></param>
        public CartesianViewerUserControlViewModel(Grid mainViewBox, IEventAggregator eventAggregator)
        {
            RedrawCanvasWhenSizeChange = true;
            _mainViewBox = mainViewBox;
            UserControlLoaded = new DelegateCommand(HandleUserControlLoaded);
            UserControlSizeChanged = new DelegateCommand(HandleUserControlSizeChanged);
            eventAggregator.GetEvent<PubSubEvent<ShapesReadModel>>().Subscribe(MessageReceived);
            eventAggregator.GetEvent<PubSubEvent<string>>().Subscribe(ClearViewer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageSentModel"></param>
        private void MessageReceived(ShapesReadModel messageSentModel)
        {
            AddShapes(messageSentModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageSentModel"></param>
        private void ClearViewer(string messageSentModel)
        {
            _cartesianService.Clear();
            Scale = _cartesianService.Scale;
        }

        /// <summary>
        /// 
        /// </summary>
        public DelegateCommand UserControlLoaded { get; }

        /// <summary>
        /// 
        /// </summary>
        private void HandleUserControlLoaded()
        {

            _cartesianService = new CartesianService();
            _cartesianCanvas = _cartesianService.NewCanvas(_mainViewBox.ActualWidth, _mainViewBox.ActualHeight);
            Scale = _cartesianService.Scale;

            _mainViewBox.Children.Add(_cartesianCanvas);
        }

        /// <summary>
        /// 
        /// </summary>
        public DelegateCommand UserControlSizeChanged { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private void HandleUserControlSizeChanged()
        {
            if (!RedrawCanvasWhenSizeChange) return;
            _cartesianCanvas = _cartesianService.RedrawCanvas(_mainViewBox.ActualWidth, _mainViewBox.ActualHeight);
            Scale = _cartesianService.Scale;
        }

        CartesianCanvas _cartesianCanvas;
        CartesianService _cartesianService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shapes"></param>
        private void AddShapes(ShapesReadModel shapes)
        {

            _cartesianService.AddShapes(shapes);
            Scale = _cartesianService.Scale;
        }

    }
}
