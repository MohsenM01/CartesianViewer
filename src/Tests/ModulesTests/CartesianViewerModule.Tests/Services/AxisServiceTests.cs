using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using CartesianViewerModule.Services;
using CartesianViewerModule.Shapes;
using FluentAssertions;
using Xunit;

namespace CartesianViewerModule.Tests.Services
{
    public class AxisServiceTests : BaseUnitTest
    {
        private readonly CartesianCanvas _cartesianCanvas;
        private const int Margin = 10;
        private const int Height = 1000;
        private const int Width = 800;
        private readonly Point _basePoint;
        public AxisServiceTests()
        {
            _cartesianCanvas = new CartesianCanvas
            {
                Background = Brushes.LightSteelBlue,
                Height = Height,
                Width = Width
            };
            _basePoint = new Point(Height / 2, Height / 2);
            var axisService = new AxisService(_cartesianCanvas, _basePoint, Margin);
            axisService.DrawAxes();
        }

        [WpfFact]
        public void ItShouldHas4Children()
        {
            _cartesianCanvas.Children.Count.Should().Be(4);
        }


        [WpfFact]
        public void ItShouldDrawPositiveXAxis_StartPoint_EndPoint()
        {
            var path = (Path)_cartesianCanvas.Children[0];
            path.Name.Should().Be("PositiveXAxis");
            var geometryGroup = (GeometryGroup)path.Data;
            var lineGeometry = (LineGeometry)geometryGroup.Children[0];
            lineGeometry.StartPoint.Should().Be(new Point(_basePoint.X, _basePoint.Y));
            lineGeometry.EndPoint.Should().Be(new Point(_cartesianCanvas.Width - Margin, _basePoint.Y));
        }

        [WpfFact]
        public void ItShouldDrawNegativeXAxis_StartPoint_EndPoint()
        {
            var path = (Path)_cartesianCanvas.Children[1];
            path.Name.Should().Be("NegativeXAxis");
            var geometryGroup = (GeometryGroup)path.Data;
            var lineGeometry = (LineGeometry)geometryGroup.Children[0];
            lineGeometry.StartPoint.Should().Be(new Point(_basePoint.X, _basePoint.Y));
            lineGeometry.EndPoint.Should().Be(new Point(Margin, _basePoint.Y));
        }


        [WpfFact]
        public void ItShouldDrawPositiveYAxis_StartPoint_EndPoint()
        {
            var path = (Path)_cartesianCanvas.Children[2];
            path.Name.Should().Be("PositiveYAxis");
            var geometryGroup = (GeometryGroup)path.Data;
            var lineGeometry = (LineGeometry)geometryGroup.Children[0];
            lineGeometry.StartPoint.Should().Be(new Point(_basePoint.X, _basePoint.Y));
            lineGeometry.EndPoint.Should().Be(new Point(_basePoint.X, Margin));
        }


        [WpfFact]
        public void ItShouldDrawNegativeYAxis_StartPoint_EndPoint()
        {
            var path = (Path)_cartesianCanvas.Children[3];
            path.Name.Should().Be("NegativeYAxis");
            var geometryGroup = (GeometryGroup)path.Data;
            var lineGeometry = (LineGeometry)geometryGroup.Children[0];
            lineGeometry.StartPoint.Should().Be(new Point(_basePoint.X, _basePoint.Y));
            lineGeometry.EndPoint.Should().Be(new Point(_basePoint.X, _cartesianCanvas.Height - Margin));
        }

    }
}