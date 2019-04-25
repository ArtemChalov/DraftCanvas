using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DraftCanvas.Primitives
{
    /// <summary>
    /// 
    /// </summary>
    public class DcRectangle : IVisualObject
    {
        private readonly string _tag = "Rectangle";
        private readonly int _id = CanvasCounter.PrimitiveID;
        private bool _isSelected = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public DcRectangle(Point p1, Point p2)
        {
            Point1 = p1;
            Point2 = p2;
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
        public Point Point1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Point Point2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DrawingVisualEx GetVisual()
        {
            DrawingVisualEx visual = new DrawingVisualEx(this);

            using (DrawingContext drawingContext = visual.RenderOpen())
            {
                drawingContext.DrawRectangle(Brushes.AliceBlue, 
                    new Pen(IsSelected ? CanvasParam.PenSelectedColor : CanvasParam.PenColor, CanvasParam.Thikness / CanvasParam.Scale),
                    new Rect(new Point(Point1.X, CanvasParam.CanvasHeight - Point1.Y), new Point(Point2.X, CanvasParam.CanvasHeight - Point2.Y)));
            }

            visual.Transform = new ScaleTransform(CanvasParam.Scale, CanvasParam.Scale, 0, CanvasParam.CanvasHeight);

            return visual;
        }
    }
}
