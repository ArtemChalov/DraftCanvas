using DraftCanvas.Interfacies;
using DraftCanvas.Primitives;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DraftCanvas.Servicies
{
    /// <summary>
    /// 
    /// </summary>
    public class LeftMouseClick : ILeftMouse
    {
        private int _pointCounter = 1;
        private List<DependencyObject> _hitTestList = new List<DependencyObject>();
        private Point _oldPoint;
        private DcRectangle _fantom = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="canvas"></param>
        public void OnMouseDown(Point point, DrCanvas canvas)
        {
            //_oldPoint = point;
            //_pointCounter++;
            _hitTestList.Clear();

            // Expand the hit test area by creating a geometry centered on the hit test point.
            EllipseGeometry expandedHitTestArea = new EllipseGeometry(point, 2.0, 2.0);

            VisualTreeHelper.HitTest(canvas,
                new HitTestFilterCallback(HitTestFilter),
                new HitTestResultCallback(HitTestCallback),
                  new GeometryHitTestParameters(expandedHitTestArea));


            // Perform the hit test against a given portion of the visual object tree.
            //HitTestResult result = VisualTreeHelper.HitTest(this, e.GetPosition(this));
            if (_hitTestList.Count > 0)
            {
                foreach (var result in _hitTestList)
                {
                    if (result is DrawingVisualEx dv)
                    {
                        if (Keyboard.Modifiers != ModifierKeys.Control)
                        {
                            foreach (DrawingVisualEx item in canvas._visualsCollection)
                            {
                                if (item.IsSelected) item.IsSelected = false;
                            }
                        }
                        dv.IsSelected = true;
                        canvas.Update();
                    }
                }
            }
            else
            {
                if (Keyboard.Modifiers != ModifierKeys.Control)
                {
                    foreach (DrawingVisualEx item in canvas._visualsCollection)
                    {
                        if (item.IsSelected) item.IsSelected = false;
                    }
                    canvas.Update();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="canvas"></param>
        public void OnMouseMove(Point point, DrCanvas canvas)
        {
            //if (_pointCounter == 2)
            //{
            //    if (_fantom != null)
            //    {
            //        _fantom.Point1 = _oldPoint;
            //        _fantom.Point2 = point;
            //        canvas.Update();
            //    }
            //    else
            //    {
            //        _fantom = new DcRectangle(_oldPoint, point);
            //        canvas.AddToVisualCollection(_fantom);
            //    }
            //}
        }

        private HitTestFilterBehavior HitTestFilter(DependencyObject potentialHitTestTarget)
        {
            // Test for the object value you want to filter.
            if (potentialHitTestTarget.GetType() == typeof(DrawingVisualEx))
            {
                // Visual object is part of hit test results enumeration.
                return HitTestFilterBehavior.Continue;
            }
            else
            {
                // Visual object and descendants are NOT part of hit test results enumeration.
                return HitTestFilterBehavior.ContinueSkipSelf;
            }
        }

        private HitTestResultBehavior HitTestCallback(HitTestResult result)
        {
            _hitTestList.Add(result.VisualHit);

            return HitTestResultBehavior.Continue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        public void OnMouseUp(DrCanvas canvas)
        {
            //_pointCounter = 1;
            //if (_fantom != null)
            //{
            //    canvas.RemoveVisualObject(_fantom);
            //    _fantom = null;
            //}
        }
    }
}
