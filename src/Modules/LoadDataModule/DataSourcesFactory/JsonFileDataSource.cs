using Common.Models;
using Common.Models.Shapes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using LoadDataModule.JsonConverter;

namespace LoadDataModule.DataSourcesFactory
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonFileDataSource : IFileDataSource
    {
        /// <summary>
        /// Read data from test, validate, extract data
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ShapesReadModel GetData(string filePath)
        {
            //TODO file validation
            //TODO async
            var fileContent = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions { WriteIndented = true };
            options.Converters.Add(new JsonStringColorConverter());
            options.Converters.Add(new JsonStringPointConverter());
            options.Converters.Add(new JsonStringShapeTypeEnumConverter());
            var cartesianCircles = JsonSerializer.Deserialize<List<CartesianCircleModel>>(fileContent, options).Where(a => a.Type == ShapeTypeEnum.Circle).ToList();
            var cartesianLines = JsonSerializer.Deserialize<List<CartesianLineModel>>(fileContent, options).Where(a => a.Type == ShapeTypeEnum.Line).ToList();
            var cartesianTriangles = JsonSerializer.Deserialize<List<CartesianTriangleModel>>(fileContent, options).Where(a => a.Type == ShapeTypeEnum.Triangle).ToList();
            return new ShapesReadModel
            {
                CartesianCircles = cartesianCircles,
                CartesianLines = cartesianLines,
                CartesianTriangles = cartesianTriangles
            };
                 
        }
    }
}
