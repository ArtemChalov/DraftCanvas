using System.Windows;
using System.Windows.Media;

namespace DraftCanvas.Primitives
{
    class RectangleSelector : IVisualObject
    {
        private readonly string _tag = "Rectangle";
        private readonly int _id = -1;
        private bool _isSelected = false;
        private Point _point1;
        private Point _point2;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public RectangleSelector(Point p1, Point p2)
        {
            P1 = p1;
            P2 = p2;
        }

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
        public bool IsDirty { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool IsSelected { get { return _isSelected; } set { _isSelected = value; IsDirty = true; } }

        /// <summary>
        /// 
        /// </summary>
        public Point P1
        {
            get { return _point1; }
            set { _point1 = value; IsDirty = true; }
        }


        /// <summary>
        /// 
        /// </summary>
        public Point P2
        {
            get { return _point2; }
            set { _point2 = value; IsDirty = true; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Brush FillBrush { get; set; } = Brushes.Transparent;

        /// <summary>
        /// 
        /// </summary>
        public Brush Stroke { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        public DashStyle Dash { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DrawingVisualEx GetVisual()
        {
            DrawingVisualEx visual = new DrawingVisualEx(this);

            Brush stroke = Stroke == null ? CanvasParam.PenColor : Stroke;
            Pen pen = new Pen(IsSelected ? CanvasParam.PenSelectedColor : stroke, 0.5);
            pen.DashStyle = IsSelected ? null : Dash;

            using (DrawingContext drawingContext = visual.RenderOpen())
            {
                GuidelineSet guideline = new GuidelineSet()
                {
                    GuidelinesX = new DoubleCollection() { P1.X - 0.5 },
                    GuidelinesY = new DoubleCollection() { P1.Y - 0.5 }
                };
                drawingContext.PushGuidelineSet(guideline);

                drawingContext.DrawRectangle(FillBrush, pen,
                    new Rect(new Point(P1.X, CanvasParam.CanvasHeight - P1.Y), new Point(P2.X, CanvasParam.CanvasHeight - P2.Y)));
            }

            visual.Transform = new ScaleTransform(CanvasParam.Scale, CanvasParam.Scale, 0, CanvasParam.CanvasHeight);

            return visual;
        }
    }
}
