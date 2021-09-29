using System.Windows;
using Common.Models.Shapes;
using System.Windows.Shapes;

namespace CartesianViewerModule.Shapes.ShapesBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public class ShapeDirector : IShapeDirector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="properX"></param>
        /// <param name="properY"></param>
        /// <param name="scale"></param>
        /// <param name="mouseClickPropertyPath"></param>
        /// <returns></returns>
        public Shape GetShape(CartesianCircleModel model, double properX, double properY, double scale, string mouseClickPropertyPath = "")
        {
            var circle = new CartesianCircle(model, scale);
            circle.BuildCircle(properX, properY);
            if (!string.IsNullOrWhiteSpace(mouseClickPropertyPath))
            {
                circle.AttachEvent(mouseClickPropertyPath, "PreviewMouseLeftButtonUp");
            }
            return circle.Shape;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="properX1"></param>
        /// <param name="properY1"></param>
        /// <param name="properX2"></param>
        /// <param name="properY2"></param>
        /// <param name="mouseClickPropertyPath"></param>
        /// <returns></returns>
        public Shape GetShape(CartesianLineModel model, double properX1, double properY1, double properX2, double properY2, string mouseClickPropertyPath = "")
        {
            var line = new CartesianLine(model);
            line.BuildLine(properX1, properY1, properX2, properY2);
            if (!string.IsNullOrWhiteSpace(mouseClickPropertyPath))
            {
                line.AttachEvent(mouseClickPropertyPath, "PreviewMouseLeftButtonUp");
            }
            return line.Shape;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="mouseClickPropertyPath"></param>
        /// <returns></returns>
        public Shape GetShape(CartesianTriangleModel model, Point a, Point b, Point c, string mouseClickPropertyPath = "")
        {
            var triangle = new CartesianTriangle(model);
            triangle.BuildTriangle(a, b, c);
            if (!string.IsNullOrWhiteSpace(mouseClickPropertyPath))
            {
                triangle.AttachEvent(mouseClickPropertyPath, "PreviewMouseLeftButtonUp");
            }
            return triangle.Shape;
        }
    }
}
