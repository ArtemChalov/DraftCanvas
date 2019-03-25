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
        private double _height;
        private double _width;

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
                {_p1Hash, new Point(X1, Y1)},
                {_p2Hash, new Point(X2, Y2)}
            };

            _length = DcMath.GetDistance(X1, Y1, X2, Y2);
            _angle = DcMath.GetLineSegmentAngle(this);
            _height = DcMath.GetHeight(Y1, Y2);
            _width = DcMath.GetWidth(X1, X2);
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

            while (angle >= 360) angle -= 360;
            while (angle <= -360) angle += 360;
            _angle = angle < 0 ? angle + 360 : angle;

            _length = length;

            _x2 = _x1 + DcMath.Xoffset(length, angle);
            _y2 = _y1 + DcMath.Yoffset(length, angle);

            _height = DcMath.GetHeight(Y1, Y2);
            _width = DcMath.GetWidth(X1, X2);

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
        public DrCanvas Owner { get; set; }

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
        public double X1 { get { return _x1; } set { OnChangeP1(value, Y1); } }

        /// <summary>
        /// 
        /// </summary>
        public double Y1 { get { return _y1; } set { OnChangeP1(X1, value); } }

        /// <summary>
        /// 
        /// </summary>
        public double X2 { get { return _x2; } set { OnChangeP1(value, Y2); } }

        /// <summary>
        /// 
        /// </summary>
        public double Y2 { get { return _y2; } set { OnChangeP1(X2, value); } }

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
            get { return _height; }
            set { OnChangeHeight(value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Width
        {
            get { return _width; }
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

        // Changes the length of the DcLineSegment.
        private void OnChangeLength(double newLength)
        { // Full tested
            if (Length <= 0 || newLength == Length) return;

            // Tests if one of the point has a constraint.
            var p1HasConstraint = Owner.PointCollection[_p1Hash].ActiveHash != 0;
            var p2HasConstraint = Owner.PointCollection[_p2Hash].ActiveHash != 0;

            if (p1HasConstraint)
            {
                if (p2HasConstraint) return;
                else
                {
                    if (HasConstraint(Constraints.Heigth))
                    { // Tested
                        if (Angle == 90 || Angle == 270 || HasConstraint(Constraints.Width) || HasConstraint(Constraints.Angle) || newLength < Height) return;

                        _angle = DcMath.GetAngleByHeight(Height, newLength, X1, Y1, X2, Y2);
                        OnChangeP2(X1 + DcMath.Xoffset(newLength, _angle), Y2);
                    }
                    else if (HasConstraint(Constraints.Width))
                    { // Tested
                        if (Angle == 0 || Angle == 180 || HasConstraint(Constraints.Heigth) || HasConstraint(Constraints.Angle) || newLength < Width) return;

                        _angle = DcMath.GetAngleByWidth(Width, newLength, X1, Y1, X2, Y2);
                        OnChangeP2(X2, Y1 + DcMath.Yoffset(newLength, _angle));
                    }
                    else
                    { // Tested
                        if (newLength <= 0) return;
                        double delta = newLength - _length;
                        OnChangeP2(X2 + DcMath.Xoffset(delta, Angle), Y2 + DcMath.Yoffset(delta, Angle));
                    }
                }
            }
            else
            {
                if (p2HasConstraint)
                {
                    if (HasConstraint(Constraints.Heigth))
                    { // Tested
                        if (Angle == 90 || Angle == 270 || HasConstraint(Constraints.Width) || HasConstraint(Constraints.Angle) || newLength < Height) return;

                        _angle = DcMath.GetAngleByHeight(Height, newLength, X1, Y1, X2, Y2);
                        OnChangeP1(X2 - DcMath.Xoffset(newLength, _angle), Y1);
                    }
                    else if (HasConstraint(Constraints.Width))
                    { // Tested
                        if (Angle == 0 || Angle == 180 || HasConstraint(Constraints.Heigth) || HasConstraint(Constraints.Angle) || newLength < Width) return;

                        _angle = DcMath.GetAngleByWidth(Width, newLength, X1, Y1, X2, Y2);
                        OnChangeP1(X1, Y2 - DcMath.Yoffset(newLength, _angle));
                    }
                    else
                    {  // Tested
                        if (newLength <= 0) return;
                        double delta = newLength - _length;
                        OnChangeP1(X1 - DcMath.Xoffset(delta, Angle), Y1 - DcMath.Yoffset(delta, Angle));
                    }
                }
                else
                {
                    if (HasConstraint(Constraints.Heigth))
                    { // Tested
                        if (Angle == 90 || Angle == 270 || HasConstraint(Constraints.Width) || HasConstraint(Constraints.Angle) || newLength < Height) return;

                        double newWidth = DcMath.GetСathetus(Height, newLength);
                        double dx = X2 - X1;

                        double xOffset = (newWidth - Width) / 2;
                        xOffset = dx > 0 ? xOffset : -xOffset;

                        OnChangeP1(X1 - xOffset, Y1);
                        OnChangeP2(X2 + xOffset, Y2);
                        _angle = DcMath.GetLineSegmentAngle(this);
                    }
                    else if (HasConstraint(Constraints.Width))
                    { // Tested
                        if (Angle == 0 || Angle == 180 || HasConstraint(Constraints.Heigth) || HasConstraint(Constraints.Angle) || newLength < Width) return;

                        double newHeight = DcMath.GetСathetus(Width, newLength);
                        double dy = Y2 - Y1;

                        double yOffset = (newHeight - Height) / 2;
                        yOffset = dy > 0 ? yOffset : -yOffset;

                        OnChangeP1(X1, Y1 - yOffset);
                        OnChangeP2(X2, Y2 + yOffset);
                        _angle = DcMath.GetLineSegmentAngle(this);
                    }
                    else
                    { // Tested
                        double delta = newLength - _length;
                        OnChangeP1(X1 - DcMath.Xoffset(delta / 2, Angle), Y1 - DcMath.Yoffset(delta / 2, Angle));
                        OnChangeP2(X2 + DcMath.Xoffset(delta / 2, Angle), Y2 + DcMath.Yoffset(delta / 2, Angle));
                    }
                }
            }

            _length = newLength;
            _width = DcMath.GetWidth(X1, X2);
            _height = DcMath.GetHeight(Y1, Y2);
            AddLocalConstraint(Constraints.Length);
        }

        // Changes the heigth of the DcLineSegment
        private void OnChangeHeight(double newHeight)
        { // Full tested
            if (newHeight <= 0 || newHeight == Height) return;
            if ((HasConstraint(Constraints.Length) && HasConstraint(Constraints.Angle)) || Angle == 0 || Angle == 180) return;

            // Tests if one of the point has a constraint.
            var p1HasConstraint = Owner.PointCollection[_p1Hash].ActiveHash != 0;
            var p2HasConstraint = Owner.PointCollection[_p2Hash].ActiveHash != 0;

            if (p1HasConstraint)
            {
                if (p2HasConstraint) return;
                else
                {
                    double delta = (newHeight - Height);
                    double dy = Y2 - Y1;
                    delta = dy > 0 ? delta : -delta;
                    if (HasConstraint(Constraints.Length))
                    { // Tested
                        if (HasConstraint(Constraints.Width) || HasConstraint(Constraints.Angle)) return;
                        else
                        {
                            double newWidth = DcMath.GetСathetus(newHeight, Length);
                            double dx = X2 - X1;

                            double xOffset = newWidth - Width;
                            xOffset = dx > 0 ? xOffset : -xOffset;

                            OnChangeP2(X2 + xOffset, Y2 + delta);
                            _width = newWidth;
                            _angle = DcMath.GetLineSegmentAngle(this);
                        }
                    }
                    else if (HasConstraint(Constraints.Angle))
                    { // Tested
                        if (HasConstraint(Constraints.Width) || HasConstraint(Constraints.Length)) return;
                        else
                        {
                            double newWidth = Math.Abs(DcMath.XoffsetByTan(newHeight, Angle));
                            double xOffset = newWidth - Width;
                            double dx = X2 - X1;
                            xOffset = dx > 0 ? xOffset : -xOffset;

                            OnChangeP2(X2 + xOffset, Y2 + delta);
                            _length = DcMath.GetDistance(X1, Y1, X2, Y2);
                            _width = newWidth;
                        }
                    }
                    else
                    { // Tested
                        OnChangeP2(X2, Y2 + delta);
                        _length = DcMath.GetDistance(X1, Y1, X2, Y2);
                        _angle = DcMath.GetLineSegmentAngle(this);
                    }
                }
            }
            else
            {
                if (p2HasConstraint)
                {
                    double delta = (newHeight - Height);
                    double dy = Y2 - Y1;
                    delta = dy > 0 ? delta : -delta;
                    if (HasConstraint(Constraints.Length))
                    { // Tested
                        if (HasConstraint(Constraints.Width) || HasConstraint(Constraints.Angle)) return;
                        else
                        {
                            double newWidth = DcMath.GetСathetus(newHeight, Length);
                            double dx = X2 - X1;

                            double xOffset = newWidth - Width;
                            xOffset = dx > 0 ? xOffset : -xOffset;

                            OnChangeP1(X1 - xOffset, Y1 - delta);
                            _width = newWidth;
                            _angle = DcMath.GetLineSegmentAngle(this);
                        }
                    }
                    else if (HasConstraint(Constraints.Angle))
                    {
                        if (HasConstraint(Constraints.Width) || HasConstraint(Constraints.Length)) return;
                        else
                        { // Tested
                            double newWidth = Math.Abs(DcMath.XoffsetByTan(newHeight, Angle));
                            double xOffset = newWidth - Width;
                            double dx = X2 - X1;
                            xOffset = dx > 0 ? xOffset : -xOffset;

                            OnChangeP1(X1 - xOffset, Y1 - delta);
                            _length = DcMath.GetDistance(X1, Y1, X2, Y2);
                            _width = newWidth;
                        }
                    }
                    else
                    { // Tested
                        OnChangeP1(X1, Y1 - delta);
                        _length = DcMath.GetDistance(X1, Y1, X2, Y2);
                        _angle = DcMath.GetLineSegmentAngle(this);
                    }
                }
                else
                {
                    double delta = (newHeight - Height) / 2;
                    double dy = Y2 - Y1;
                    delta = dy > 0 ? delta : -delta;
                    if (HasConstraint(Constraints.Length))
                    {  // Tested
                        if (HasConstraint(Constraints.Width) || HasConstraint(Constraints.Angle)) return;
                        else
                        {
                            double newWidth = DcMath.GetСathetus(newHeight, Length);
                            double dx = X2 - X1;

                            double xOffset = (newWidth - Width) / 2;
                            xOffset = dx > 0 ? xOffset : -xOffset;

                            OnChangeP1(X1 - xOffset, Y1 - delta);
                            OnChangeP2(X2 + xOffset, Y2 + delta);
                            _width = newWidth;
                            _angle = DcMath.GetLineSegmentAngle(this);
                        }
                    }
                    else if (HasConstraint(Constraints.Angle))
                    { // Tested
                        if (HasConstraint(Constraints.Width) || HasConstraint(Constraints.Length)) return;
                        else
                        {
                            double newWidth = Math.Abs(DcMath.XoffsetByTan(newHeight, Angle));
                            double xOffset = (newWidth - Width) / 2;
                            double dx = X2 - X1;
                            xOffset = dx > 0 ? xOffset : -xOffset;

                            OnChangeP1(X1 - xOffset, Y1 - delta);
                            OnChangeP2(X2 + xOffset, Y2 + delta);
                            _width = newWidth;
                            _length = DcMath.GetDistance(X1, Y1, X2, Y2);
                        }
                    }
                    else
                    { // Tested
                        OnChangeP1(X1, Y1 - delta);
                        OnChangeP2(X2, Y2 + delta);
                        _angle = DcMath.GetLineSegmentAngle(this);
                        _length = DcMath.GetDistance(X1, Y1, X2, Y2);
                    }
                }
            }

            AddLocalConstraint(Constraints.Heigth);
            _height = newHeight;
        }

        // Changes the width of the DcLineSegment
        private void OnChangeWidth(double newWidth)
        { // Full tested
            if (newWidth <= 0 || newWidth == Width) return;
            if ((HasConstraint(Constraints.Length) && HasConstraint(Constraints.Angle)) || Angle == 90 || Angle == 270) return;

            // Tests if one of the point has a constraint.
            var p1HasConstraint = Owner.PointCollection[_p1Hash].ActiveHash != 0;
            var p2HasConstraint = Owner.PointCollection[_p2Hash].ActiveHash != 0;

            if (p1HasConstraint)
            {
                if (p2HasConstraint) return;
                else
                {
                    double delta = (newWidth - Width);
                    double dx = X2 - X1;
                    delta = dx > 0 ? delta : -delta;
                    if (HasConstraint(Constraints.Length))
                    { // Tested
                        if (HasConstraint(Constraints.Heigth) || HasConstraint(Constraints.Angle)) return;
                        else
                        {
                            double newHeight = DcMath.GetСathetus(newWidth, Length);
                            double dy = Y2 - Y1;

                            double yOffset = newHeight - Height;
                            yOffset = dy > 0 ? yOffset : -yOffset;

                            OnChangeP2(X2 + delta, Y2 + yOffset);
                            _height = newHeight;
                            _angle = DcMath.GetLineSegmentAngle(this);
                        }
                    }
                    else if (HasConstraint(Constraints.Angle))
                    { // Tested
                        if (HasConstraint(Constraints.Heigth) || HasConstraint(Constraints.Length)) return;
                        else
                        {
                            double newHeigth = Math.Abs(DcMath.YoffsetByTan(newWidth, Angle));
                            double yOffset = newHeigth - Height;
                            double dy = Y2 - Y1;
                            yOffset = dy > 0 ? yOffset : -yOffset;

                            OnChangeP2(X2 + delta, Y2 + yOffset);
                            _length = DcMath.GetDistance(X1, Y1, X2, Y2);
                            _height = newHeigth;
                        }
                    }
                    else
                    { // Tested
                        OnChangeP2(X2 + delta, Y2);
                        _length = DcMath.GetDistance(X1, Y1, X2, Y2);
                        _angle = DcMath.GetLineSegmentAngle(this);
                    }
                }
            }
            else
            {
                if (p2HasConstraint)
                {
                    double delta = (newWidth - Width);
                    double dx = X2 - X1;
                    delta = dx > 0 ? delta : -delta;
                    if (HasConstraint(Constraints.Length))
                    { // Tested
                        if (HasConstraint(Constraints.Heigth) || HasConstraint(Constraints.Angle)) return;
                        else
                        {
                            double newHeigth = DcMath.GetСathetus(newWidth, Length);
                            double dy = Y2 - Y1;

                            double yOffset = newHeigth - Height;
                            yOffset = dy > 0 ? yOffset : -yOffset;

                            OnChangeP1(X1 - delta, Y1 - yOffset);
                            _height = newHeigth;
                            _angle = DcMath.GetLineSegmentAngle(this);
                        }
                    }
                    else if (HasConstraint(Constraints.Angle))
                    { // Tested
                        if (HasConstraint(Constraints.Heigth) || HasConstraint(Constraints.Length)) return;
                        else
                        {
                            double newHeight = Math.Abs(DcMath.YoffsetByTan(newWidth, Angle));
                            double yOffset = newHeight - Height;
                            double dy = Y2 - Y1;
                            yOffset = dy > 0 ? yOffset : -yOffset;

                            OnChangeP1(X1 - delta, Y1 - yOffset);
                            _length = DcMath.GetDistance(X1, Y1, X2, Y2);
                            _height = newHeight;
                        }
                    }
                    else
                    { // Tested
                        OnChangeP1(X1 - delta, Y1);
                        _length = DcMath.GetDistance(X1, Y1, X2, Y2);
                        _angle = DcMath.GetLineSegmentAngle(this);
                    }
                }
                else
                {
                    double delta = (newWidth - Width) / 2;
                    double dx = X2 - X1;
                    delta = dx > 0 ? delta : -delta;
                    if (HasConstraint(Constraints.Length))
                    {
                        if (HasConstraint(Constraints.Heigth) || HasConstraint(Constraints.Angle)) return;
                        else
                        { // Tested
                            double newHeigth = DcMath.GetСathetus(newWidth, Length);
                            double dy = Y2 - Y1;

                            double yOffset = (newHeigth - Height) / 2;
                            yOffset = dy > 0 ? yOffset : -yOffset;

                            OnChangeP1(X1 - delta, Y1 - yOffset);
                            OnChangeP2(X2 + delta, Y2 + yOffset);
                            _height = newHeigth;
                            _angle = DcMath.GetLineSegmentAngle(this);
                        }
                    }
                    else if (HasConstraint(Constraints.Angle))
                    { // Tested
                        if (HasConstraint(Constraints.Heigth) || HasConstraint(Constraints.Length)) return;
                        else
                        {
                            double newHeigth = Math.Abs(DcMath.YoffsetByTan(newWidth, Angle));
                            double yOffset = (newHeigth - Height) / 2;
                            double dY = Y2 - Y1;
                            yOffset = dY > 0 ? yOffset : -yOffset;

                            OnChangeP1(X1 - delta, Y1 - yOffset);
                            OnChangeP2(X2 + delta, Y2 + yOffset);
                            _height = newHeigth;
                            _length = DcMath.GetDistance(X1, Y1, X2, Y2);
                        }
                    }
                    else
                    { // Tested
                        OnChangeP1(X1 - delta, Y1);
                        OnChangeP2(X2 + delta, Y2);
                        _angle = DcMath.GetLineSegmentAngle(this);
                        _length = DcMath.GetDistance(X1, Y1, X2, Y2);
                    }
                }
            }

            AddLocalConstraint(Constraints.Width);
            _width = newWidth;
        }

        // Changes the angle of the DcLineSegment
        private void OnChangeAngle(double newAngle)
        { // Full tested
            while (newAngle >= 360) newAngle -= 360;
            while (newAngle <= -360) newAngle += 360;
            newAngle = newAngle < 0 ? newAngle + 360 : newAngle;

            // Tests if one of the point has a constraint.
            var p1HasConstraint = Owner.PointCollection[_p1Hash].ActiveHash != 0;
            var p2HasConstraint = Owner.PointCollection[_p2Hash].ActiveHash != 0;

            if (p2HasConstraint) return;
            else
            {
                if (HasConstraint(Constraints.Heigth) || HasConstraint(Constraints.Width)) return;
                else
                { // Tested
                    if (newAngle == 0)
                    {
                        OnChangeP2(X1 + Length, Y1);
                        _height = 0;
                        _width = Length;
                    }
                    else if (newAngle == 180)
                    {
                        OnChangeP2(X1 - Length, Y1);
                        _height = 0;
                        _width = Length;
                    }
                    else if (newAngle == 90)
                    {
                        OnChangeP2(X1, Y1 + Length);
                        _height = Length;
                        _width = 0;
                    }
                    else if (newAngle == 270)
                    {
                        OnChangeP2(X1, Y1 - Length);
                        _height = Length;
                        _width = 0;
                    }
                    else
                    {
                        double xOffset = DcMath.Xoffset(Length, newAngle);
                        double yOffset = DcMath.Yoffset(Length, newAngle);

                        OnChangeP2(X1 + xOffset, Y1 + yOffset);
                    }
                }
            }

            _angle = newAngle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool HasConstraint(Constraints constraint)
        {
            return (LocalConstraint & (int)constraint) != 0;
        }

        #endregion
    }
}
