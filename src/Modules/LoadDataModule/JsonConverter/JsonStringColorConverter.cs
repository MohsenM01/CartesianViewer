using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Media;

namespace LoadDataModule.JsonConverter
{
    /// <summary>
    /// create Color converter for the JSON serialization classes
    /// See <see href="https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-converters-how-to?pivots=dotnet-5-0">HERE</see>
    /// </summary>
    public class JsonStringColorConverter : JsonConverter<Color>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (value == null)
            {
                throw new Exception("");
            }

            // TODO Extention method
            //TODO Seperate validation
            var vlueSplited = value.Split(";");
            if (vlueSplited.Length != 4)
            {
                throw new Exception("");
            }

            if (!byte.TryParse(vlueSplited[0], out byte aValue))
            {
                throw new Exception("");
            }
            if (!byte.TryParse(vlueSplited[1], out byte rValue))
            {
                throw new Exception("");
            }
            if (!byte.TryParse(vlueSplited[2], out byte gValue))
            {
                throw new Exception("");
            }
            if (!byte.TryParse(vlueSplited[3], out byte bValue))
            {
                throw new Exception("");
            }

            return Color.FromArgb(aValue, rValue, gValue, bValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {

            writer.WriteStringValue(value.ToString());

        }
    }
}
