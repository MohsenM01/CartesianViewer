using System.Windows.Controls;
using Common.Models.Shapes;
using System.Windows.Media;
using System.Windows.Shapes;
using CartesianViewerModule.Shapes.Events;

namespace CartesianViewerModule.Shapes.ShapesBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public class CartesianCircle : IShapeBuilder
    {
        private readonly CartesianCircleModel _circle;
        private readonly Ellipse _adpteeCircle;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="circle"></param>
        /// <param name="scale"></param>
        public CartesianCircle(CartesianCircleModel circle, double scale)
        {
            _circle = circle;
            var solidColorBrush = new SolidColorBrush
            {
                Color = circle.Color
            };

            _adpteeCircle = new Ellipse
            {
                StrokeThickness = 2,
                Stroke = solidColorBrush,

                Width = circle.Radius * scale,
                Height = circle.Radius * scale,

            };
            if (circle.Filled)
            {
                _adpteeCircle.Fill = solidColorBrush;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properX"></param>
        /// <param name="properY"></param>
        public void BuildCircle(double properX, double properY)
        {
            Canvas.SetLeft(_adpteeCircle, properX - _circle.Radius / 2);
            Canvas.SetTop(_adpteeCircle, properY - _circle.Radius / 2);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyPath"></param>
        /// <param name="eventName"></param>
        public void AttachEvent(string propertyPath, string eventName)
        {
            _adpteeCircle.SetShapeTrigger(propertyPath, eventName);
        }

        /// <summary>
        /// 
        /// </summary>
        public Shape Shape => _adpteeCircle;

    }
}