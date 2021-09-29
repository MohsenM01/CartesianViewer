using System.Text.Json.Serialization;

namespace Common.Models.Shapes.Bases
{
    /// <summary>
    /// This abstract class is used to provide a common, implemented functionality among all the implementations of the component => ShapeTypeEnum.
    /// Each new shape type should be inherit
    /// </summary>
    public abstract class CartesianShapeModel : ICartesianShapeModel
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("type")]
        public ShapeTypeEnum Type { get; set; }
    }
}
