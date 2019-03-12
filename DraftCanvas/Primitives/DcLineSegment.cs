using DraftCanvas.Servicies;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using PM = DraftCanvas.Servicies.PointManager;

namespace DraftCanvas.Primitives
{
    /// <summary>
    /// 
    /// </summary>
    public class DcLineSegment : IPrimitive
    {
        private readonly string _tag = "LineSegment";
        private readonly int _id = CanvasCounter.PrimitiveID;
        private readonly IDictionary<int, Point> _points;

        private double _x1;
        private double _y1;
        private double _x2;
        private double _y2;
        private double _length;
        private double _angle;

        private int _p1Hash;
        private int _p2Hash;

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public DcLineSegment(double x1, double y1, double x2, double y2)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;

            _p1Hash = PointHash.CreateHash(1, _id);
            _p2Hash = PointHash.CreateHash(2, _id);

            _points = new Dictionary<int, Point>()
            {
                {_p1Hash, new Point(_x1, _y1)},
                {_p2Hash, new Point(_x2, _y2)}
            };

            _length = DcMath.GetDistance(_x1, _y1, _x2, _y2);
            _angle = DcMath.GetLineSegmentAngle(this);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="length"></param>
        /// <param name="angle"></param>
        public DcLineSegment(Point originPoint,  double length, double angle)
        {
            _x1 = originPoint.X;
            _y1 = originPoint.Y;
            _angle = angle;
            _length = length;

            _x2 = _x1 + DcMath.Xoffset(length, angle);
            _y2 = _y1 + DcMath.Yoffset(length, angle);

            _p1Hash = PointHash.CreateHash(1, _id);
            _p2Hash = PointHash.CreateHash(2, _id);

            _points = new Dictionary<int, Point>()
            {
                {_p1Hash, new Point(_x1, _y1)},
                {_p2Hash, new Point(_x2, _y2)}
            };
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Canvas Owner { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ID => _id;

        /// <summary>
        /// 
        /// </summary>
        public string Tag => _tag;

        /// <summary>
        /// 
        /// </summary>
        public double X1 => _x1;

        /// <summary>
        /// 
        /// </summary>
        public double Y1 => _y1;

        /// <summary>
        /// 
        /// </summary>
        public double X2 => _x2;

        /// <summary>
        /// 
        /// </summary>
        public double Y2 => _y2;

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<int, Point> Points => _points;

        /// <summary>
        /// 
        /// </summary>
        public double Length
        {
            get { return _length; }
            set {OnChangeLength(value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Height
        {
            get { return DcMath.GetHeight(_y1, _y2); }
            set { OnChangeHeight(value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Width
        {
            get { return DcMath.GetWidth(_x1, _x2); }
            set { OnChangeWidth(value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Angle
        {
            get { return _angle; }
            set { OnChangeAngle(value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int LocalConstraint { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public bool IsDirty { get; set; } = false;

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        /// <param name="pointHash"></param>
        /// <returns></returns>
        public bool SetPoint(double newX, double newY, int pointHash)
        {
            double DelataX = 0;
            double DelataY = 0;
            bool res = false;
            switch (PointHash.GetPointIndex(pointHash))
            {
                case 1:
                    DelataX = newX - X1;
                    DelataY = newY - Y1;
                    res = OnChangeP1(newX, newY);
                    if (LocalConstraint != 0)
                        res = OnChangeP2(X2 + DelataX, Y2 + DelataY);
                    break;
                case 2:
                    DelataX = newX - X2;
                    DelataY = newY - Y2;
                    res = OnChangeP2(newX, newY);
                    if (LocalConstraint != 0)
                        res = OnChangeP1(X1 + DelataX, Y1 + DelataY);
                    break;
            }
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DrawingVisualEx GetVisual()
        {
            DrawingVisualEx visual = new DrawingVisualEx(this);

            using (DrawingContext drawingContext = visual.RenderOpen())
            {
                drawingContext.DrawLine(new Pen(CanvasParam.PenColor, CanvasParam.Thikness / CanvasParam.Scale),
                    new Point(X1, CanvasParam.CanvasHeight - Y1), new Point(X2, CanvasParam.CanvasHeight - Y2));
            }

            visual.Transform = new ScaleTransform(CanvasParam.Scale, CanvasParam.Scale, 0, CanvasParam.CanvasHeight);

            return visual;
        }

        #endregion

        #region Private Methods

        private bool OnChangeP1(double newX, double newY)
        {
            bool res = false;
            if (Owner.PointCollection[_p1Hash].DependedHash != 0)
                res = PM.ResolveConstraint(Owner, newX, newY, _p1Hash);

            _x1 = newX;
            _y1 = newY;

            IsDirty = true;

            return res;
        }

        private bool OnChangeP2(double newX, double newY)
        {
            bool res = false;
            if (Owner.PointCollection[_p2Hash].DependedHash != 0)
                res = PM.ResolveConstraint(Owner, newX, newY, _p2Hash);

            _x2 = newX;
            _y2 = newY;

            IsDirty = true;

            return res;
        }

        private void OnChangeLength(double newValue)
        {
            double delta = newValue -_length;

            // Tests if one of the point has a constraint.
            var p1HasConstraint = Owner.PointCollection[_p1Hash].ActiveHash != 0;
            var p2HasConstraint = Owner.PointCollection[_p2Hash].ActiveHash != 0;

            if (p1HasConstraint)
            {
                if (p2HasConstraint) return;
                else
                {
                    double x1 = X2 + DcMath.Xoffset(delta, Angle);
                    double y1 = Y2 + DcMath.Yoffset(delta, Angle);

                    double h = DcMath.GetHeight(y1, Y2);
                    double w = DcMath.GetWidth(x1, X2);

                    if (HasConstraint(LineConstraint.Heigth) && Angle != 0 && Angle != 180)
                    {
                        if (h != Height)
                        {

                        }
                    }

                    OnChangeP2(X2 + DcMath.Xoffset(delta, Angle), Y2 + DcMath.Yoffset(delta, Angle));
                }
            }
            else
            {
                if (p2HasConstraint)
                    OnChangeP1(X1 - DcMath.Xoffset(delta, Angle), Y1 - DcMath.Yoffset(delta, Angle));
                else
                {
                    if (LocalConstraint == 0)
                    {
                        OnChangeP1(X1 - DcMath.Xoffset(delta / 2, Angle), Y1 - DcMath.Yoffset(delta / 2, Angle));
                        OnChangeP2(X2 + DcMath.Xoffset(delta / 2, Angle), Y2 + DcMath.Yoffset(delta / 2, Angle));
                    }
                    else
                    {
                       //if (LocalConstraint & LineConstraint.)
                    }
                }
            }

            _length = newValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool HasConstraint(LineConstraint constraint)
        {
            return (LocalConstraint & (int)constraint) != 0;
        }

        private void OnChangeHeight(double value)
        {
            throw new NotImplementedException();
        }

        private void OnChangeWidth(double value)
        {
            throw new NotImplementedException();
        }

        private void OnChangeAngle(double newValue)
        {
            var p2HasConstraint = Owner.PointCollection[_p2Hash].ActiveHash != 0;

            if (!p2HasConstraint)
            {
                _angle = newValue;
                _x2 = _x1 + DcMath.Xoffset(Length, newValue);
                _y2 = _y1 + DcMath.Yoffset(Length, newValue);
                IsDirty = true;
            }
        }

        #endregion
    }
}
