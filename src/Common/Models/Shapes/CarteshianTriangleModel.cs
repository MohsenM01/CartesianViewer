using Common.Models.Shapes.Bases;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Media;

namespace Common.Models.Shapes
{
    /// <summary>
    /// Line properties definition
    /// <example>
    /// {
    ///    "type": "triangle",
    ///    "a": "-15;-20",
    ///    "b": "15;-20,3",
    ///    "c": "0;21",
    ///    "filled": true,
    /// }  "color": "127;255;0;255"
    /// </example>
    /// </summary>
    public class CartesianTriangleModel : CartesianShapeModel
    {
        /// <summary>
        /// 
        /// </summary>

        [JsonPropertyName("a")]
        public Point A { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("b")]
        public Point B { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("c")]
        public Point C { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("filled")]
        public bool Filled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("color")]
        public Color Color { get; set; }
    }
}
