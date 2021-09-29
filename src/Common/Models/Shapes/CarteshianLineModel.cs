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
    ///    "type": "line",
    ///    "a": "-1,5; 3,4",
    ///    "b": "2,2; 5,7",
    ///    "color": "127;255;255;255"
    ///}
    /// </example>
    /// </summary>
    public class CartesianLineModel : CartesianShapeModel
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
        [JsonPropertyName("color")]
        public Color Color { get; set; }
    }
}
