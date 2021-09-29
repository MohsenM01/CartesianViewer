using Common.Models.Shapes.Bases;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Media;

namespace Common.Models.Shapes
{
    /// <summary>
    /// Circle properties definition
    /// <example>
    /// {
    ///    "type": "circle",
    ///    "center": "0;0",
    ///    "radius": 15.0,
    ///    "filled": false,
    ///    "color": "127;255;0;0"
    /// }
    /// </example>
    /// </summary>
    public class CartesianCircleModel : CartesianShapeModel
    {
        /// <summary>
        /// 
        /// </summary>

        [JsonPropertyName("center")]
        public Point Center { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("radius")]
        public double Radius { get; set; }

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
