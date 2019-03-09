using DraftCanvas.Servicies;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace DraftCanvas.Primitives
{
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

        public DcLineSegment(double x1, double y1, double x2, double y2, LineConstraint constraint = LineConstraint.Free)
        {
            LocalConstraint = constraint;

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

        
        public DcLineSegment(Point originPoint,  double length, double angle, LineConstraint constraint = LineConstraint.Angle)
        {
            LocalConstraint = constraint;

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

        public Canvas Owner { get; set; }

        public int ID => _id;

        public string Tag => _tag;

        public double X1 => _x1;

        public double Y1 => _y1;

        public double X2 => _x2;

        public double Y2 => _y2;

        public IDictionary<int, Point> Points => _points;

        public double Length
        {
            get { return _length; }
            set {OnChangeLength(value); }
        }

        public double Angle
        {
            get { return _angle; }
            set { OnChangeAngle(value);
            }
        }

        public LineConstraint LocalConstraint { get; set; } = LineConstraint.Free;

        public bool IsDirty { get; set; } = false;

        #endregion

        #region Public Methods

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
                    if (LocalConstraint != LineConstraint.Free)
                        res = OnChangeP2(X2 + DelataX, Y2 + DelataY);
                    break;
                case 2:
                    DelataX = newX - X2;
                    DelataY = newY - Y2;
                    res = OnChangeP2(newX, newY);
                    if (LocalConstraint != LineConstraint.Free)
                        res = OnChangeP1(X1 + DelataX, Y1 + DelataY);
                    break;
            }
            return res;
        }

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
            if (Owner.PointCollection[_p1Hash].SubHash != 0)
                res = PointManager.ResolveConstraint(Owner, newX, newY, _p1Hash);

            _x1 = newX;
            _y1 = newY;

            IsDirty = true;

            return res;
        }

        private bool OnChangeP2(double newX, double newY)
        {
            bool res = false;
            if (Owner.PointCollection[_p2Hash].SubHash != 0)
                res = PointManager.ResolveConstraint(Owner, newX, newY, _p2Hash);

            _x2 = newX;
            _y2 = newY;

            IsDirty = true;

            return res;
        }

        private void OnChangeLength(double newValue)
        {
            double delta = newValue -_length;
            
            var p1HasConstraint = Owner.PointCollection[_p1Hash].IssuerHash != 0;
            var p2HasConstraint = Owner.PointCollection[_p2Hash].IssuerHash != 0;

            if (p1HasConstraint)
            {
                if (p2HasConstraint)
                {
                    return;
                }
                else
                    OnChangeP2(X2 + DcMath.Xoffset(delta, Angle), Y2 + DcMath.Yoffset(delta, Angle));
            }
            else
            {
                if (p2HasConstraint)
                    OnChangeP1(X1 - DcMath.Xoffset(delta, Angle), Y1 - DcMath.Yoffset(delta, Angle));
                else
                {
                    OnChangeP1(X1 - DcMath.Xoffset(delta / 2, Angle), Y1 - DcMath.Yoffset(delta / 2, Angle));
                    OnChangeP2(X2 + DcMath.Xoffset(delta / 2, Angle), Y2 + DcMath.Yoffset(delta / 2, Angle));
                }
            }

            _length = newValue;
        }

        private void OnChangeAngle(double newValue)
        {

        }

        #endregion
    }
}
