using System.Windows;
using Common.Models.Shapes;
using System.Windows.Shapes;

namespace CartesianViewerModule.Shapes.ShapesBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public interface IShapeDirector
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
        Shape GetShape(CartesianCircleModel model, double properX, double properY, double scale,
            string mouseClickPropertyPath);

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
        Shape GetShape(CartesianLineModel model, double properX1, double properY1, double properX2, double properY2, string mouseClickPropertyPath = "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="mouseClickPropertyPath"></param>
        /// <returns></returns>
        Shape GetShape(CartesianTriangleModel model, Point a, Point b, Point c, string mouseClickPropertyPath = "");
    }
}
