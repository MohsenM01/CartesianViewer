using Common.Models.Shapes;
using System.Collections.Generic;

namespace Common.Models
{
    /// <summary>
    /// Introducing all shapes
    /// Each new shape type should be introduce there
    /// </summary>
    public class ShapesReadModel
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<CartesianCircleModel> CartesianCircles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<CartesianLineModel> CartesianLines { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<CartesianTriangleModel> CartesianTriangles { get; set; }
    }
}
