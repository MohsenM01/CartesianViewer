using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using CartesianViewerModule.Shapes;

namespace CartesianViewerModule.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class AxisService
    {

        private readonly CartesianCanvas _cartesianCanvas;
        private readonly Point _basePoint;
        private readonly int _margin;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartesianCanvas"></param>
        /// <param name="basePoint"></param>
        /// <param name="margin"></param>
        public AxisService(CartesianCanvas cartesianCanvas, Point basePoint, int margin)
        {
            _cartesianCanvas = cartesianCanvas;
            _basePoint = basePoint;
            _margin = margin;
        }

        /// <summary>
        /// 
        /// </summary>
        public void DrawAxes()
        {
            _cartesianCanvas.Children.Clear();
            DrawPositiveXAxis();
            DrawNegativeXAxis();
            DrawPositiveYAxis();
            DrawNegativeYAxis();
        }

        #region PositiveXAxis

        /// <summary>
        /// 
        /// </summary>
        private void DrawPositiveXAxis()
        {
            var positiveXAxisStartPoint = _basePoint;
            var positiveXAxisEndPoint = PositiveXAxisEndPoint();
            var step = GetXStep();
            var halfHeight = GetDegreeHeight() / 2;

            var y = (int)positiveXAxisStartPoint.Y;
            var xMin = (int)positiveXAxisStartPoint.X + step;
            var xMax = (int)positiveXAxisEndPoint.X - step;

            var geometryGroup = new GeometryGroup();
            geometryGroup.Children.Add(new LineGeometry(positiveXAxisStartPoint, positiveXAxisEndPoint));
            for (int x = xMin; x < xMax; x += step)
            {
                geometryGroup.Children.Add(new LineGeometry(
                    new Point(x, y - halfHeight),
                    new Point(x, y + halfHeight)));
            }

            var path = new Path
            {
                StrokeThickness = 1,
                Stroke = Brushes.Black,
                Data = geometryGroup,
                Name = "PositiveXAxis"
            };

            _cartesianCanvas.Children.Add(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Point PositiveXAxisEndPoint()
        {
            var middleOfPage = _basePoint;
            int margin = _margin;
            return new Point(GetCanvasWidth() - margin, middleOfPage.Y);
        }

        #endregion

        #region NegativeXAxis

        /// <summary>
        /// 
        /// </summary>
        private void DrawNegativeXAxis()
        {
            var negativeXAxisStartPoint = _basePoint;
            var negativeXAxisEndPoint = NegativeXAxisEndPoint();
            var step = GetXStep();
            var halfHeight = GetDegreeHeight() / 2;

            var y = (int)negativeXAxisStartPoint.Y;
            var xMin = (int)negativeXAxisStartPoint.X - step;
            var xMax = (int)negativeXAxisEndPoint.X + step;

            var geometryGroup = new GeometryGroup();
            geometryGroup.Children.Add(new LineGeometry(negativeXAxisStartPoint, negativeXAxisEndPoint));

            for (int x = xMin; x >= xMax; x -= step)
            {
                geometryGroup.Children.Add(new LineGeometry(
                    new Point(x, y - halfHeight),
                    new Point(x, y + halfHeight)));
            }

            var path = new Path
            {
                StrokeThickness = 1,
                Stroke = Brushes.Black,
                Data = geometryGroup,
                Name = "NegativeXAxis"
            };

            _cartesianCanvas.Children.Add(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Point NegativeXAxisEndPoint()
        {
            var middleOfPage = _basePoint;
            return new Point(_margin, middleOfPage.Y);
        }

        #endregion

        #region PositiveYAxis

        /// <summary>
        /// 
        /// </summary>
        private void DrawPositiveYAxis()
        {
            var positiveXAxisStartPoint = _basePoint;
            var positiveXAxisEndPoint = PositiveYAxisEndPoint();
            var step = GetYStep();
            var halfHeight = GetDegreeWidth() / 2;

            var x = (int)positiveXAxisStartPoint.X;
            var yMin = (int)positiveXAxisStartPoint.Y - step;
            var yMax = (int)positiveXAxisEndPoint.Y + step;

            var geometryGroup = new GeometryGroup();
            geometryGroup.Children.Add(new LineGeometry(positiveXAxisStartPoint, positiveXAxisEndPoint));
            for (int y = yMin; y >= yMax; y -= step)
            {
                geometryGroup.Children.Add(new LineGeometry(
                    new Point(x - halfHeight, y),
                    new Point(x + halfHeight, y)));
            }

            var path = new Path
            {
                StrokeThickness = 1,
                Stroke = Brushes.Black,
                Data = geometryGroup,
                Name = "PositiveYAxis"
            };

            _cartesianCanvas.Children.Add(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Point PositiveYAxisEndPoint()
        {
            var middleOfPage = _basePoint;
            return new Point(middleOfPage.X, _margin);
        }

        #endregion

        #region NegativeYAxis

        /// <summary>
        /// 
        /// </summary>
        private void DrawNegativeYAxis()
        {
            var negativeXAxisStartPoint = _basePoint;
            var negativeXAxisEndPoint = NegativeYAxisEndPoint();
            var step = GetYStep();
            var halfHeight = GetDegreeWidth() / 2;

            var x = (int)negativeXAxisStartPoint.X;
            var yMin = (int)negativeXAxisStartPoint.Y + step;
            var yMax = (int)negativeXAxisEndPoint.Y - step;

            var geometryGroup = new GeometryGroup();
            geometryGroup.Children.Add(new LineGeometry(negativeXAxisStartPoint, negativeXAxisEndPoint));
            for (int y = yMin; y <= yMax; y += step)
            {
                geometryGroup.Children.Add(new LineGeometry(
                    new Point(x - halfHeight, y),
                    new Point(x + halfHeight, y)));
            }

            var path = new Path
            {
                StrokeThickness = 1,
                Stroke = Brushes.Black,
                Data = geometryGroup,
                Name = "NegativeYAxis"
            };

            _cartesianCanvas.Children.Add(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Point NegativeYAxisEndPoint()
        {
            var middleOfPage = _basePoint;
            return new Point(middleOfPage.X, GetCanvasHeight() - _margin);
        }

        #endregion

        #region Setting

        private double GetCanvasHeight()
        {
            return _cartesianCanvas.Height;
        }

        private double GetCanvasWidth()
        {
            return _cartesianCanvas.Width;
        }

        private int GetXStep()
        {
            return 10;
        }

        private int GetYStep()
        {
            return 10;
        }

        private int GetDegreeHeight()
        {
            return 5;
        }

        private int GetDegreeWidth()
        {
            return 5;
        }

        #endregion

    }
}
