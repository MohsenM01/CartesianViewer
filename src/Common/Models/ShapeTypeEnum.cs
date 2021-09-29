using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Common.Models
{
    /// <summary>
    /// Introducing all shapes
    /// Each new shape type should be introduce there
    /// </summary>
    [Description]
    public enum ShapeTypeEnum
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("line")]
        Line,

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("circle")]
        Circle,

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("triangle")]
        Triangle
    }
}
