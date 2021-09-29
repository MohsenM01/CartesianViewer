using System.Windows;
using Common.Models.Shapes;
using System.Windows.Media;
using System.Windows.Shapes;
using CartesianViewerModule.Shapes.Events;

namespace CartesianViewerModule.Shapes.ShapesBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public class CartesianTriangle : IShapeBuilder
    {
        private readonly Polygon _adpteePolygon;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="triangle"></param>
        public CartesianTriangle(CartesianTriangleModel triangle)
        {
            var solidColorBrush = new SolidColorBrush
            {
                Color = triangle.Color
            };
            _adpteePolygon = new Polygon
            {
                Stroke = solidColorBrush,
                StrokeThickness = 2,
            };

            if (triangle.Filled)
            {
                _adpteePolygon.Fill = solidColorBrush;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public void BuildTriangle(Point a, Point b, Point c)
        {
            _adpteePolygon.Points.Add(a);
            _adpteePolygon.Points.Add(b);
            _adpteePolygon.Points.Add(c);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyPath"></param>
        /// <param name="eventName"></param>
        public void AttachEvent(string propertyPath, string eventName)
        {
            _adpteePolygon.SetShapeTrigger(propertyPath, eventName);
        }

        /// <summary>
        /// 
        /// </summary>
        public Shape Shape => _adpteePolygon;
    }
}

