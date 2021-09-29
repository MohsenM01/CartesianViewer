using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using CartesianViewerModule.Models;
using CartesianViewerModule.Shapes;
using CartesianViewerModule.Shapes.ShapesBuilder;
using Common.Models;
using Common.Models.Shapes;

namespace CartesianViewerModule.Services
{
    /// <summary>
    /// Optimizing Performance: 2D Graphics and Imaging <see href="https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-4.0/bb613591(v=vs.100)?redirectedfrom=MSDN">HERE</see>
    /// </summary>
    public class CartesianService
    {
        private readonly CartesianCanvas _cartesianCanvas;
        private readonly ShapeDirector _shapeDirector;
        private IList<ShapeHolderModel> _shapesHolder;
        private double _scale;

        /// <summary>
        /// 
        /// </summary>
        public double Scale => _scale;

        /// <summary>
        /// 
        /// </summary>
        public CartesianService()
        {
            _cartesianCanvas = new CartesianCanvas
            {
                Background = Brushes.LightSteelBlue,
            };

            _shapeDirector = new ShapeDirector();
            _shapesHolder = new List<ShapeHolderModel>();
            _scale = 1;
        }

        /// <summary>
        /// renew canvas
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public CartesianCanvas NewCanvas(double width, double height)
        {
            _cartesianCanvas.Height = height;
            _cartesianCanvas.Width = width;
            _cartesianCanvas.Children.Clear();
            _shapesHolder.Clear();
            //CalculateStartPoint();
            CalculateBestScale();
            DrawAxes();
            return _cartesianCanvas;
        }

        /// <summary>
        /// redraw canvas and all children
        /// </summary>
        /// <returns></returns>
        public CartesianCanvas RedrawCanvas(double width, double height)
        {
            _cartesianCanvas.Height = height;
            _cartesianCanvas.Width = width;
            _cartesianCanvas.Children.Clear();
            //CalculateStartPoint();
            CalculateBestScale();
            DrawAxes();
            foreach (var shapeItem in _shapesHolder)
            {
                if (shapeItem.CartesianShape.Type == ShapeTypeEnum.Circle)
                {
                    DrawCircle(shapeItem.CartesianShape as CartesianCircleModel, shapeItem.Name, shapeItem.BiningEvents.Any() ? shapeItem.BiningEvents.First().Item2 : null);
                }
                if (shapeItem.CartesianShape.Type == ShapeTypeEnum.Line)
                {
                    DrawLine(shapeItem.CartesianShape as CartesianLineModel, shapeItem.Name, shapeItem.BiningEvents.Any() ? shapeItem.BiningEvents.First().Item2 : null);
                }
                if (shapeItem.CartesianShape.Type == ShapeTypeEnum.Triangle)
                {
                    DrawTriangle(shapeItem.CartesianShape as CartesianTriangleModel, shapeItem.Name, shapeItem.BiningEvents.Any() ? shapeItem.BiningEvents.First().Item2 : null);
                }
            }
            return _cartesianCanvas;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            _shapesHolder.Clear();
            //CalculateStartPoint();
            CalculateBestScale();
            var shapes = GetDrawnShapes();
            foreach (var shape in shapes)
            {
                _cartesianCanvas.Children.Remove(shape);
            }
        }

        private List<Shape> GetDrawnShapes()
        {
            var shapes = new List<Shape>();
            const int mainShapesNumber = 4;
            var counter = 0;
            foreach (Shape shape in _cartesianCanvas.Children)
            {
                counter++;
                if (counter <= mainShapesNumber) continue;
                shapes.Add(shape);
            }

            return shapes;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="shapes"></param>
        public void AddShapes(ShapesReadModel shapes)
        {
            Clear();
            //CalculateStartPoint(shapes);
            CalculateBestScale(shapes);
            DrawAxes();
            foreach (var item in shapes.CartesianCircles)
            {
                DrawCartesianCircle(item);
            }

            foreach (var item in shapes.CartesianLines)
            {
                DrawCartesianLine(item);
            }

            foreach (var item in shapes.CartesianTriangles)
            {
                DrawCartesianTriangle(item);
            }

        }

        #region Circle

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartesianCircle"></param>
        /// <param name="mouseClickPropertyPath"></param>
        /// <returns>given unique name</returns>
        public string DrawCartesianCircle(CartesianCircleModel cartesianCircle, string mouseClickPropertyPath = "")
        {
            var name = "c" + DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture);
            _shapesHolder.Add(new ShapeHolderModel
            {
                CartesianShape = cartesianCircle,
                Name = name,
                BiningEvents = new List<Tuple<string, string>>
                {
                    new(mouseClickPropertyPath,"PreviewMouseLeftButtonUp")
                }
            });
            DrawCircle(cartesianCircle, mouseClickPropertyPath);
            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartesianCircle"></param>
        /// <param name="name"></param>
        /// <param name="mouseClickPropertyPath"></param>
        /// <returns></returns>
        private void DrawCircle(CartesianCircleModel cartesianCircle, string name, string mouseClickPropertyPath = "")
        {
            var newShape = _shapeDirector.GetShape(cartesianCircle, GetProperX(cartesianCircle.Center.X),
                GetProperY(cartesianCircle.Center.Y), _scale, mouseClickPropertyPath);
            if (newShape == null) throw new NullReferenceException(nameof(newShape));

            //HACK -it should go to own Builder class
            Canvas.SetLeft(newShape, GetProperX(cartesianCircle.Center.X) - cartesianCircle.Radius * _scale / 2);
            Canvas.SetTop(newShape, GetProperY(cartesianCircle.Center.Y) - cartesianCircle.Radius * _scale / 2);
            newShape.Name = name;
            _cartesianCanvas.Children.Add(newShape);
        }
        #endregion

        #region Line

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartesianLine"></param>
        /// <param name="mouseClickPropertyPath"></param>
        public string DrawCartesianLine(CartesianLineModel cartesianLine, string mouseClickPropertyPath = "")
        {
            var name = "l" + DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture);
            _shapesHolder.Add(new ShapeHolderModel
            {
                CartesianShape = cartesianLine,
                Name = name,
                BiningEvents = new List<Tuple<string, string>>
                {
                    new(mouseClickPropertyPath,"PreviewMouseLeftButtonUp")
                }
            });
            DrawLine(cartesianLine, name, mouseClickPropertyPath);
            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartesianLine"></param>
        /// <param name="name"></param>
        /// <param name="mouseClickPropertyPath"></param>
        private void DrawLine(CartesianLineModel cartesianLine, string name, string mouseClickPropertyPath = "")
        {
            var newShape = _shapeDirector.GetShape(cartesianLine, GetProperX(cartesianLine.A.X),
                GetProperY(cartesianLine.A.Y),
                GetProperX(cartesianLine.B.X), GetProperY(cartesianLine.B.Y),
                mouseClickPropertyPath);
            if (newShape == null) throw new NullReferenceException(nameof(newShape));

            _cartesianCanvas.Children.Add(newShape);
            newShape.Name = name;
        }

        #endregion

        #region Triangle
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartesianTriangle"></param>
        /// <param name="mouseClickPropertyPath"></param>
        public string DrawCartesianTriangle(CartesianTriangleModel cartesianTriangle, string mouseClickPropertyPath = "")
        {
            var name = "t" + DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture);
            _shapesHolder.Add(new ShapeHolderModel
            {
                CartesianShape = cartesianTriangle,
                Name = name,
                BiningEvents = new List<Tuple<string, string>>
                {
                    new(mouseClickPropertyPath,"PreviewMouseLeftButtonUp")
                }
            });
            DrawTriangle(cartesianTriangle, name, mouseClickPropertyPath);
            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartesianTriangle"></param>
        /// <param name="name"></param>
        /// <param name="mouseClickPropertyPath"></param>
        private void DrawTriangle(CartesianTriangleModel cartesianTriangle, string name, string mouseClickPropertyPath = "")
        {
            var newShape = _shapeDirector.GetShape(cartesianTriangle,
                new Point(GetProperX(cartesianTriangle.A.X), GetProperY(cartesianTriangle.A.Y)),
                new Point(GetProperX(cartesianTriangle.B.X), GetProperY(cartesianTriangle.B.Y)),
                new Point(GetProperX(cartesianTriangle.C.X), GetProperY(cartesianTriangle.C.Y)),
                mouseClickPropertyPath);
            if (newShape == null) throw new NullReferenceException(nameof(newShape));

            _cartesianCanvas.Children.Add(newShape);
            newShape.Name = name;
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double GetProperX(double x)
        {
            var middleOfPage = GetMiddleOfPage();
            return (middleOfPage.X + x * _scale);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        private double GetProperY(double y)
        {
            var middleOfPage = GetMiddleOfPage();
            return (middleOfPage.Y - y * _scale);
        }

        /// <summary>
        /// 
        /// </summary>
        private void DrawAxes()
        {
            var service = new AxisService(_cartesianCanvas, GetMiddleOfPage(), GetMargin());
            service.DrawAxes();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetMargin()
        {
            return 10;
        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public Point GetStartPoint()
        //{
        //    //_startPoint.X = _startPoint.X * _scale;
        //    //_startPoint.Y = _startPoint.Y * _scale;
        //    return _startPoint;
        //}

        //private Point _startPoint;

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public void CalculateStartPoint(ShapesReadModel shapes = null)
        //{
        //    var corners = new List<Point>();

        //    if (ShapesHolder.Count > 0)
        //    {
        //        foreach (var shapeItem in _shapesHolder)
        //        {
        //            if (shapeItem.CartesianShape.Type == ShapeTypeEnum.Circle)
        //            {
        //                var item = shapeItem.CartesianShape as CartesianCircleModel;
        //                var centerPoint = new Point(item.Center.X, item.Center.Y);
        //                corners.Add(new Point(centerPoint.X - item.Radius / 2, centerPoint.Y));
        //                corners.Add(new Point(centerPoint.X + item.Radius / 2, centerPoint.Y));
        //                corners.Add(new Point(centerPoint.X, centerPoint.Y - item.Radius / 2));
        //                corners.Add(new Point(centerPoint.X, centerPoint.Y + item.Radius / 2));
        //            }
        //            if (shapeItem.CartesianShape.Type == ShapeTypeEnum.Line)
        //            {
        //                var item = shapeItem.CartesianShape as CartesianLineModel;
        //                corners.Add(item.A);
        //                corners.Add(item.B);
        //            }
        //            if (shapeItem.CartesianShape.Type == ShapeTypeEnum.Triangle)
        //            {
        //                var item = shapeItem.CartesianShape as CartesianTriangleModel;
        //                corners.Add(item.A);
        //                corners.Add(item.B);
        //                corners.Add(item.C);
        //            }
        //        }
        //    }
        //    else if (shapes != null)
        //    {
        //        foreach (var item in shapes.CartesianCircles)
        //        {
        //            var centerPoint = new Point(item.Center.X, item.Center.Y);
        //            corners.Add(new Point(centerPoint.X - item.Radius / 2, centerPoint.Y));
        //            corners.Add(new Point(centerPoint.X + item.Radius / 2, centerPoint.Y));
        //            corners.Add(new Point(centerPoint.X, centerPoint.Y - item.Radius / 2));
        //            corners.Add(new Point(centerPoint.X, centerPoint.Y + item.Radius / 2));
        //        }

        //        foreach (var item in shapes.CartesianLines)
        //        {
        //            corners.Add(item.A);
        //            corners.Add(item.B);
        //        }

        //        foreach (var item in shapes.CartesianTriangles)
        //        {
        //            corners.Add(item.A);
        //            corners.Add(item.B);
        //            corners.Add(item.C);
        //        }
        //    }

        //    var middleOfPage = GetMiddleOfPage();
        //    double? minNegativeX = null;
        //    double? maxPositiveY = null;
        //    foreach (var corner in corners)
        //    {
        //        if (corner.X < 0)
        //        {
        //            if (!minNegativeX.HasValue || corner.X < minNegativeX)
        //                minNegativeX = corner.X;
        //        }
        //        if (corner.Y > 0)
        //        {
        //            if (!maxPositiveY.HasValue || corner.Y > maxPositiveY)
        //                maxPositiveY = corner.Y;
        //        }
        //    }

        //    if (minNegativeX.HasValue && middleOfPage.X < -1 * minNegativeX.Value)
        //    {
        //        middleOfPage.X = -1 * minNegativeX.Value + GetMargin();
        //    }

        //    if (maxPositiveY.HasValue && middleOfPage.Y < maxPositiveY.Value)
        //    {
        //        //middleOfPage.Y = maxPositiveY.Value + GetMargin();
        //    }
        //    _startPoint = middleOfPage;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Point GetMiddleOfPage()
        {
            var margin = GetMargin();
            var xPoint = (int)_cartesianCanvas.Width / 2 - margin;
            var yPoint = (int)_cartesianCanvas.Height / 2 - margin;
            return new Point(xPoint, yPoint);
        }

        /// <summary>
        /// 
        /// </summary>
        public IList<ShapeHolderModel> ShapesHolder => _shapesHolder;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uniqName"></param>
        /// <returns></returns>
        public Shape GetShapeByName(string uniqName)
        {
            foreach (Shape cartesianCanvasChild in _cartesianCanvas.Children)
            {
                if (cartesianCanvasChild.Name == uniqName)
                    return cartesianCanvasChild;
            }

            return null;
        }


        private List<Point> GetPoints(ShapesReadModel shapes = null)
        {
            var corners = new List<Point>();
            if (ShapesHolder.Count > 0)
            {
                foreach (var shapeItem in _shapesHolder)
                {
                    if (shapeItem.CartesianShape.Type == ShapeTypeEnum.Circle)
                    {
                        var item = shapeItem.CartesianShape as CartesianCircleModel;
                        var centerPoint = new Point(GetProperX(item.Center.X), GetProperX(item.Center.Y));
                        corners.Add(new Point(centerPoint.X - item.Radius / 2, centerPoint.Y));
                        corners.Add(new Point(centerPoint.X + item.Radius / 2, centerPoint.Y));
                        corners.Add(new Point(centerPoint.X, centerPoint.Y - item.Radius / 2));
                        corners.Add(new Point(centerPoint.X, centerPoint.Y + item.Radius / 2));
                    }
                    if (shapeItem.CartesianShape.Type == ShapeTypeEnum.Line)
                    {
                        var item = shapeItem.CartesianShape as CartesianLineModel;
                        corners.Add(new Point(GetProperX(item.A.X), GetProperY(item.A.Y)));
                        corners.Add(new Point(GetProperX(item.B.X), GetProperY(item.B.Y)));
                    }
                    if (shapeItem.CartesianShape.Type == ShapeTypeEnum.Triangle)
                    {
                        var item = shapeItem.CartesianShape as CartesianTriangleModel;
                        corners.Add(new Point(GetProperX(item.A.X), GetProperY(item.A.Y)));
                        corners.Add(new Point(GetProperX(item.B.X), GetProperY(item.B.Y)));
                        corners.Add(new Point(GetProperX(item.C.X), GetProperY(item.C.Y)));
                    }
                }
            }
            if (shapes != null)
            {
                foreach (var item in shapes.CartesianCircles)
                {
                    var centerPoint = new Point(GetProperX(item.Center.X), GetProperX(item.Center.Y));
                    corners.Add(new Point(centerPoint.X - item.Radius / 2, centerPoint.Y));
                    corners.Add(new Point(centerPoint.X + item.Radius / 2, centerPoint.Y));
                    corners.Add(new Point(centerPoint.X, centerPoint.Y - item.Radius / 2));
                    corners.Add(new Point(centerPoint.X, centerPoint.Y + item.Radius / 2));
                }

                foreach (var item in shapes.CartesianLines)
                {
                    corners.Add(new Point(GetProperX(item.A.X), GetProperY(item.A.Y)));
                    corners.Add(new Point(GetProperX(item.B.X), GetProperY(item.B.Y)));
                }

                foreach (var item in shapes.CartesianTriangles)
                {
                    corners.Add(new Point(GetProperX(item.A.X), GetProperY(item.A.Y)));
                    corners.Add(new Point(GetProperX(item.B.X), GetProperY(item.B.Y)));
                    corners.Add(new Point(GetProperX(item.C.X), GetProperY(item.C.Y)));
                }

            }

            return corners;
        }

        private void CalculateBestScale(ShapesReadModel shapes = null)
        {
            _scale = 1;
            var corners = new List<Point>();
            if (ShapesHolder.Count > 0)
            {
                foreach (var shapeItem in _shapesHolder)
                {
                    if (shapeItem.CartesianShape.Type == ShapeTypeEnum.Circle)
                    {
                        var item = shapeItem.CartesianShape as CartesianCircleModel;
                        var centerPoint = new Point(GetProperX(item.Center.X), GetProperY(item.Center.Y));
                        corners.Add(new Point(centerPoint.X - item.Radius / 2, centerPoint.Y));
                        corners.Add(new Point(centerPoint.X + item.Radius / 2, centerPoint.Y));
                        corners.Add(new Point(centerPoint.X, centerPoint.Y - item.Radius / 2));
                        corners.Add(new Point(centerPoint.X, centerPoint.Y + item.Radius / 2));
                    }
                    if (shapeItem.CartesianShape.Type == ShapeTypeEnum.Line)
                    {
                        var item = shapeItem.CartesianShape as CartesianLineModel;
                        corners.Add(new Point(GetProperX(item.A.X), GetProperY(item.A.Y)));
                        corners.Add(new Point(GetProperX(item.B.X), GetProperY(item.B.Y)));
                    }
                    if (shapeItem.CartesianShape.Type == ShapeTypeEnum.Triangle)
                    {
                        var item = shapeItem.CartesianShape as CartesianTriangleModel;
                        corners.Add(new Point(GetProperX(item.A.X), GetProperY(item.A.Y)));
                        corners.Add(new Point(GetProperX(item.B.X), GetProperY(item.B.Y)));
                        corners.Add(new Point(GetProperX(item.C.X), GetProperY(item.C.Y)));
                    }
                }
            }
            if (shapes != null)
            {
                foreach (var item in shapes.CartesianCircles)
                {
                    var centerPoint = new Point(GetProperX(item.Center.X), GetProperY(item.Center.Y));
                    corners.Add(new Point(centerPoint.X - item.Radius / 2, centerPoint.Y));
                    corners.Add(new Point(centerPoint.X + item.Radius / 2, centerPoint.Y));
                    corners.Add(new Point(centerPoint.X, centerPoint.Y - item.Radius / 2));
                    corners.Add(new Point(centerPoint.X, centerPoint.Y + item.Radius / 2));
                }

                foreach (var item in shapes.CartesianLines)
                {
                    corners.Add(new Point(GetProperX(item.A.X), GetProperY(item.A.Y)));
                    corners.Add(new Point(GetProperX(item.B.X), GetProperY(item.B.Y)));
                }

                foreach (var item in shapes.CartesianTriangles)
                {
                    corners.Add(new Point(GetProperX(item.A.X), GetProperY(item.A.Y)));
                    corners.Add(new Point(GetProperX(item.B.X), GetProperY(item.B.Y)));
                    corners.Add(new Point(GetProperX(item.C.X), GetProperY(item.C.Y)));
                }

            }

            var middleOfPage = GetMiddleOfPage();

            double bigestPositiveX = middleOfPage.X;
            double smallestNegativeX = middleOfPage.X;

            double bigestPositiveY = middleOfPage.Y;
            double smallestNegativeY = middleOfPage.Y;

            foreach (var corner in corners)
            {
                if (corner.X < smallestNegativeX)
                    smallestNegativeX = corner.X;

                if (corner.X > bigestPositiveX)
                    bigestPositiveX = corner.X;

                if (corner.Y > smallestNegativeY)
                    smallestNegativeY = corner.Y;

                if (corner.Y < bigestPositiveY)
                    bigestPositiveY = corner.Y;
            }

            if (_cartesianCanvas.Width - GetMargin() < bigestPositiveX)
            {
                var newScale = (_cartesianCanvas.Width - 8 * GetMargin()) / bigestPositiveX;
                if (newScale < _scale)
                {
                    _scale = newScale;
                }
            }

            if (_cartesianCanvas.Height - GetMargin() < smallestNegativeY)
            {
                var newScale = (_cartesianCanvas.Height - 8 * GetMargin()) / smallestNegativeY;
                if (newScale < _scale)
                {
                    _scale = newScale;
                }
            }

            if (smallestNegativeX < 0)
            {
                var newScale = (_cartesianCanvas.Width - 8 * GetMargin()) / (_cartesianCanvas.Width - smallestNegativeX);
                if (newScale < _scale)
                {
                    _scale = newScale;
                }
            }

            if (bigestPositiveY < 0)
            {
                var newScale = (_cartesianCanvas.Height - 8 * GetMargin()) / (_cartesianCanvas.Height - bigestPositiveY);
                if (newScale < _scale)
                {
                    _scale = newScale;
                }
            }
            if (corners.Count == 0)
            {
                _scale = 1;
                return;
            }
        }


    }
}
