using Common.Models.Shapes;
using System.Windows.Media;
using System.Windows.Shapes;
using CartesianViewerModule.Shapes.Events;

namespace CartesianViewerModule.Shapes.ShapesBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public class CartesianLine : IShapeBuilder
    {
        private readonly Line _adpteeLine;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        public CartesianLine(CartesianLineModel line)
        {
            var solidColorBrush = new SolidColorBrush
            {
                Color = line.Color
            };

            _adpteeLine = new Line
            {
                Fill = solidColorBrush,
                Stroke = solidColorBrush,
                StrokeThickness = 2,

                X1 = line.A.X,
                Y1 = line.A.Y,

                X2 = line.B.X,
                Y2 = line.B.Y
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properX1"></param>
        /// <param name="properY1"></param>
        /// <param name="properX2"></param>
        /// <param name="properY2"></param>
        public void BuildLine(double properX1, double properY1, double properX2, double properY2)
        {
            _adpteeLine.X1 = properX1;
            _adpteeLine.Y1 = properY1;
            _adpteeLine.X2 = properX2;
            _adpteeLine.Y2 = properY2;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyPath"></param>
        /// <param name="eventName"></param>
        public void AttachEvent(string propertyPath, string eventName)
        {
            _adpteeLine.SetShapeTrigger(propertyPath, eventName);
        }

        /// <summary>
        /// 
        /// </summary>
        public Shape Shape => _adpteeLine;
    }
}