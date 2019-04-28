using DraftCanvas.Interfacies;
using DraftCanvas.Primitives;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DraftCanvas.LeftMouseAction
{
    /// <summary>
    /// 
    /// </summary>
    public class LeftMouseClick : ILeftMouse
    {
        private int _pointCounter = 1;
        private List<DependencyObject> _hitTestList = new List<DependencyObject>();
        private Point _oldPoint;
        private RectangleSelector _fantom = null;
        BrushConverter _converter = new BrushConverter();
        private bool _intersects = false;
        private DrawingVisualEx _cuurent_selected_item = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="canvas"></param>
        public void OnMouseDown(Point point, DrCanvas canvas)
        {
            _oldPoint.X = point.X;
            _oldPoint.Y = CanvasParam.CanvasHeight - point.Y;
            _pointCounter++;
            _hitTestList.Clear();

            // Expand the hit test area by creating a geometry centered on the hit test point.
            EllipseGeometry expandedHitTestArea = new EllipseGeometry(point, 2.0, 2.0);

            VisualTreeHelper.HitTest(canvas,
                new HitTestFilterCallback(HitTestFilter),
                new HitTestResultCallback(HitTestCallback),
                  new GeometryHitTestParameters(expandedHitTestArea));


            // Perform the hit test against a given portion of the visual object tree.
            if (_hitTestList.Count > 0)
            {
                foreach (DrawingVisualEx result in _hitTestList)
                {
                    if (Keyboard.Modifiers != ModifierKeys.Control)
                    {
                        // Sets all items to a non-selected state.
                        foreach (DrawingVisualEx item in canvas._visualsCollection)
                        {
                            if (item.IsSelected) item.IsSelected = false;
                        }
                        result.IsSelected = true;
                    }
                    else
                    {
                        // Inverts selection state
                        result.IsSelected = !result.IsSelected;
                        _cuurent_selected_item = result;
                    }
                    canvas.Update();
                }
            }
            else
            {
                _cuurent_selected_item = null;
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
            if (_pointCounter == 2)
            {
                if (_fantom != null)
                {
                    _fantom.P1 = new Point(_oldPoint.X, _oldPoint.Y);
                    _fantom.P2 = new Point(point.X, CanvasParam.CanvasHeight - point.Y);
                    _fantom.FillBrush = (point.X - _oldPoint.X) < 0
                        ? (Brush)_converter.ConvertFromString("#3200FF00") : (Brush)_converter.ConvertFromString("#320000FF");
                    canvas.Update();
                }
                else
                {
                    // Avoids noise
                    if (Math.Abs(_oldPoint.X - point.X) > 5 && Math.Abs(_oldPoint.Y - point.Y) > 5)
                    {
                        _fantom = new RectangleSelector(_oldPoint, new Point(point.X, CanvasParam.CanvasHeight - point.Y));
                        _fantom.Stroke = Brushes.DarkBlue;
                        canvas.AddToVisualCollection(_fantom);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        public void OnMouseUp(DrCanvas canvas)
        {
            _pointCounter = 1;
            if (_fantom != null)
            {
                Rect rect = new Rect(new Point(_fantom.P1.X, CanvasParam.CanvasHeight - _fantom.P1.Y),
                    new Point(_fantom.P2.X, CanvasParam.CanvasHeight - _fantom.P2.Y));
                // Expand the hit test area by creating a geometry centered on the hit test point.
                var expandedHitTestArea = new RectangleGeometry(rect);

                // Enables intersect option
                if (_fantom.P1.X > _fantom.P2.X) _intersects = true;

                // Phantom is no longer needed
                canvas.RemoveVisualObject(_fantom);
                _fantom = null;

                _hitTestList.Clear();

                // Avoids noise
                if (rect.Height <= 5 && rect.Width <= 5) return;

                VisualTreeHelper.HitTest(canvas,
                new HitTestFilterCallback(HitTestFilter),
                new HitTestResultCallback(HitTestCallback2),
                  new GeometryHitTestParameters(expandedHitTestArea));

                // Perform the hit test against a given portion of the visual object tree.
                if (_hitTestList.Count > 0)
                {
                    foreach (DrawingVisualEx result in _hitTestList)
                    {
                        if (Keyboard.Modifiers == ModifierKeys.Control)
                        {
                            if (_cuurent_selected_item != null && _cuurent_selected_item.ID == result.ID) continue;
                            result.IsSelected = !result.IsSelected;
                        }
                        else
                            result.IsSelected = true;
                        canvas.Update();
                    }
                }
            }

            _intersects = false;
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

        private HitTestResultBehavior HitTestCallback2(HitTestResult result)
        {
            // Retrieve the results of the hit test.
            IntersectionDetail intersectionDetail = ((GeometryHitTestResult)result).IntersectionDetail;

            switch (intersectionDetail)
            {
                case IntersectionDetail.FullyContains:

                    return HitTestResultBehavior.Continue;

                case IntersectionDetail.Intersects:

                    if (_intersects) _hitTestList.Add(result.VisualHit);
                    // Set the behavior to return visuals at all z-order levels.
                    return HitTestResultBehavior.Continue;

                case IntersectionDetail.FullyInside:

                    // Add the hit test result to the list that will be processed after the enumeration.
                    _hitTestList.Add(result.VisualHit);
                    // Set the behavior to return visuals at all z-order levels.
                    return HitTestResultBehavior.Continue;

                default:
                    return HitTestResultBehavior.Stop;
            }
        }
    }
}
