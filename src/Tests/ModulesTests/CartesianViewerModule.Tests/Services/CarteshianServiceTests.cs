using CartesianViewerModule.Services;
using CartesianViewerModule.Shapes;
using Common.Models.Shapes;
using FluentAssertions;
using Xunit;

namespace CartesianViewerModule.Tests.Services
{
    public class CartesianServiceTests : BaseUnitTest
    {
        private readonly CartesianService _cartesianService;
        private const int Margin = 10;
        private const int Width = 800;
        private const int Height = 1000;
        private readonly CartesianCanvas _cartesianCanvas;

        public CartesianServiceTests()
        {
            _cartesianService = new CartesianService();
            _cartesianCanvas = _cartesianService.NewCanvas(Width, Height);
        }

        [WpfFact]
        public void ItShouldHas4Children()
        {
            _cartesianCanvas.Children.Count.Should().Be(4);
        }


        [WpfFact]
        public void ItShouldDrawPositiveXAxis_StartPoint_EndPoint()
        {
            _cartesianService.DrawCartesianLine(new CartesianLineModel(), "l1");
            
        }
       
    }
}