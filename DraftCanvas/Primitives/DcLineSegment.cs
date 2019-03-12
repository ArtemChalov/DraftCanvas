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
            _angle = angle < 0 ? angle + 360 : angle;
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
        /// Adds a new local constraint.
        /// </summary>
        /// <param name="constraint">A desired constraint to add.</param>
        public void AddLocalConstraint(Constraints constraint)
        {
            if (constraint == Constraints.Heigth && (Angle == 0 || Angle == 180)) return;
            if (constraint == Constraints.Width && (Angle == 90 || Angle == 270)) return;

            LocalConstraint = LocalConstraint | (int)constraint;
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

        private void OnChangeLength(double newLength)
        {
            if (HasConstraint(Constraints.Length) || Length < 0) return;

            double delta = newLength -_length;

            // Tests if one of the point has a constraint.
            var p1HasConstraint = Owner.PointCollection[_p1Hash].ActiveHash != 0;
            var p2HasConstraint = Owner.PointCollection[_p2Hash].ActiveHash != 0;

            if (p1HasConstraint)
            {
                if (p2HasConstraint) return;
                else
                {
                    if (HasConstraint(Constraints.Heigth))
                    {
                        if (Angle == 90 || Angle == 270 || HasConstraint(Constraints.Width) || HasConstraint(Constraints.Angle) || newLength < Height) return;

                        _angle = DcMath.GetAngleByHeight(Height, Width, newLength);
                        OnChangeP2(X1 + DcMath.Xoffset(newLength, _angle), Y2);
                    }
                    else if (HasConstraint(Constraints.Width))
                    {
                        if (Angle == 0 || Angle == 180 || HasConstraint(Constraints.Heigth) || HasConstraint(Constraints.Angle) || newLength < Width) return;

                        _angle = DcMath.GetAngleByWidth(Width, Height, newLength);
                        OnChangeP2(X2, Y1 + DcMath.Yoffset(newLength, _angle));
                    }
                    else
                    {
                        if (newLength <= 0) return;
                        OnChangeP2(X2 + DcMath.Xoffset(delta, Angle), Y2 + DcMath.Yoffset(delta, Angle));
                    }
                }
            }
            else
            {
                if (p2HasConstraint)
                {
                    if (HasConstraint(Constraints.Heigth))
                    {
                        if (Angle == 90 || Angle == 270 || HasConstraint(Constraints.Width) || HasConstraint(Constraints.Angle) || newLength < Height) return;

                        _angle = DcMath.GetAngleByHeight(Height, Width, newLength);
                        OnChangeP1(X2 - DcMath.Xoffset(newLength, _angle), Y1);
                    }
                    else if (HasConstraint(Constraints.Width))
                    {
                        if (Angle == 0 || Angle == 180 || HasConstraint(Constraints.Heigth) || HasConstraint(Constraints.Angle) || newLength < Width) return;

                        _angle = DcMath.GetAngleByWidth(Width, Height, newLength);
                        OnChangeP1(X1, Y2 - DcMath.Yoffset(newLength, _angle));
                    }
                    else
                    {
                        if (newLength <= 0) return;

                        OnChangeP1(X1 - DcMath.Xoffset(delta, Angle), Y1 - DcMath.Yoffset(delta, Angle));
                    }
                }
                else
                {
                    if (HasConstraint(Constraints.Heigth))
                    {
                        if (Angle == 90 || Angle == 270 || HasConstraint(Constraints.Width) || HasConstraint(Constraints.Angle) || newLength < Height) return;

                        double newWidth = DcMath.GetСathetus(Height, newLength);
                        newWidth = Width > 0 ? newWidth : -newWidth;
                        double xOffset = (newWidth - Width) / 2;

                        OnChangeP1(X1 - xOffset, Y1);
                        OnChangeP2(X2 + xOffset, Y2);
                        _angle = DcMath.GetLineSegmentAngle(this);
                    }
                    else if (HasConstraint(Constraints.Width))
                    {
                        if (Angle == 0 || Angle == 180 || HasConstraint(Constraints.Heigth) || HasConstraint(Constraints.Angle) || newLength < Width) return;

                        double newHeight = DcMath.GetСathetus(Width, newLength);
                        newHeight = Height > 0 ? newHeight : -newHeight;
                        double yOffset = (newHeight - Height) / 2;

                        OnChangeP1(X1, Y1 - yOffset);
                        OnChangeP2(X2, Y2 + yOffset);
                        _angle = DcMath.GetLineSegmentAngle(this);
                    }
                    else
                    {
                        OnChangeP1(X1 - DcMath.Xoffset(delta / 2, Angle), Y1 - DcMath.Yoffset(delta / 2, Angle));
                        OnChangeP2(X2 + DcMath.Xoffset(delta / 2, Angle), Y2 + DcMath.Yoffset(delta / 2, Angle));
                    }
                }
            }

            _length = newLength;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool HasConstraint(Constraints constraint)
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
