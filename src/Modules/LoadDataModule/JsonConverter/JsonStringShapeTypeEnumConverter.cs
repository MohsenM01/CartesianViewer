using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Models;

namespace LoadDataModule.JsonConverter
{
    /// <summary>
    /// create ShapeTypeEnum converter for the JSON serialization classes
    /// See <see href="https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-converters-how-to?pivots=dotnet-5-0">HERE</see>
    /// </summary>
    public class JsonStringShapeTypeEnumConverter : JsonConverter<ShapeTypeEnum>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override ShapeTypeEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (value == null) throw new NullReferenceException(nameof(value));

            if (value.Equals("line", StringComparison.OrdinalIgnoreCase))
            {
                return ShapeTypeEnum.Line;
            }

            if (value.Equals("circle", StringComparison.OrdinalIgnoreCase))
            {
                return ShapeTypeEnum.Circle;
            }

            if (value.Equals("triangle", StringComparison.OrdinalIgnoreCase))
            {
                return ShapeTypeEnum.Triangle;
            }
            throw new NotSupportedException($"`{value}` can't be converted to `ShapeTypeEnum`.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, ShapeTypeEnum value, JsonSerializerOptions options)
        {

            writer.WriteStringValue(value.ToString());

        }
    }
}
