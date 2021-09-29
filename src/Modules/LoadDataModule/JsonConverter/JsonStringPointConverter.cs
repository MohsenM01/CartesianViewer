using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace LoadDataModule.JsonConverter
{
    /// <summary>
    /// create Point converter for the JSON serialization classes
    /// See <see href="https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-converters-how-to?pivots=dotnet-5-0">HERE</see>
    /// </summary>
    public class JsonStringPointConverter : JsonConverter<Point>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (value == null)
            {
                throw new Exception("");
            }
            var vlueSplited = value.Split(";");
            if (vlueSplited.Length != 2)
            {
                throw new Exception("");
            }

            if (!double.TryParse(vlueSplited[0].Replace(",", "."), out double xValue))
            {
                throw new Exception("");
            }
            if (!double.TryParse(vlueSplited[1].Replace(",", "."), out double yValue))
            {
                throw new Exception("");
            }
            return new Point(xValue, yValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, Point value, JsonSerializerOptions options)
        {

            writer.WriteStringValue(value.ToString());

        }
    }
}
