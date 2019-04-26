using DraftCanvas.LeftMouseAction;
using DraftCanvas.ExtendedClasses;
using DraftCanvas.Interfacies;
using DraftCanvas.Primitives;
using DraftCanvas.Servicies;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DraftCanvas
{
    /// <summary>
    /// Defines an area within you can explicitly position child elements by using
    /// coordinates that are relative to the Canvas area.
    /// </summary>
    public class DrCanvas : FrameworkElement
    {
        internal List<Visual> _visualsCollection;
        private readonly DcLineSegmentList _lineSegments;
        private readonly Dictionary<int, DcPoint> _pointCollection = new Dictionary<int, DcPoint>();
        private IPrimitiveCreator _primitiveCreator = null;
        private ILeftMouse _leftmouse = null;

        #region DependencyProperties Registration

        /// <summary>
        /// Gets or sets a Brush that is used to fill the Canvas area
        /// </summary>
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(DrCanvas), (PropertyMetadata)new FrameworkPropertyMetadata((object)null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        #endregion

        /// <summary>
        /// Initialize a new instance of the Canvas class with disired width an heigth.
        /// </summary>
        /// <param name="width">A Canvas width.</param>
        /// <param name="height">A Canvas height.</param>
        public DrCanvas(double width, double height)
        {
            this.Width = width;
            this.Height = height;

            _visualsCollection = new List<Visual>();
            ClipToBounds = true;

            CanvasParam.CanvasHeight = this.Height;

            _lineSegments = new DcLineSegmentList(this);

            _leftmouse = new LeftMouseClick();
        }

        #region Properties

         ///<summary>
         /// Gets or sets a Brush that is used to fill the Canvas area
         ///</summary>
        public Brush Background
        {
            get { return (Brush)this.GetValue(DrCanvas.BackgroundProperty); }
            set { SetValue(BackgroundProperty, (object)value); }
        }

        /// <summary>
        /// The collection of the points primitives.
        /// </summary>
        public Dictionary<int, DcPoint> PointCollection => _pointCollection;

        /// <summary>
        /// The collection of the LineSegments.
        /// </summary>
        public DcLineSegmentList DcLineSegments => _lineSegments;

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        public void DelSelectedPrimitive()
        {
            //// Sets all items to a non-selected state.
            //foreach (DrawingVisualEx item in _visualsCollection)
            //{
            //    if (item.IsSelected) RemoveVisualObject(item);
            //}
        }

        /// <summary>
        /// Adds a new visual child to the visual coolection.
        /// </summary>
        /// <param name="visualElement">A new visual element.</param>
        public void AddToVisualCollection(IVisualizable visualElement)
        {
            visualElement.Owner = this;

            if (visualElement is IPrimitive primitive)
            {
                PointManager.AddPrimitivePoints(PointCollection, primitive);
            }
            Visual visual = visualElement.GetVisual();
            _visualsCollection.Add(visual);
            AddVisualChild(visual);
        }

        /// <summary>
        /// Updates UI with visual elements which have the property IsDirty value is true.
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < _visualsCollection.Count; i++)
            {
                if (((DrawingVisualEx)_visualsCollection[i]).IsDirty)
                {
                    DrawingVisualEx dv = (DrawingVisualEx)_visualsCollection[i];
                    RemoveVisualChild(dv);
                    _visualsCollection[i] = dv.VisualObject.GetVisual();
                    dv = null;

                    ((DrawingVisualEx)_visualsCollection[i]).IsDirty = false;
                    AddVisualChild(_visualsCollection[i]);
                }
            }
        }

        /// <summary>
        /// Sets the state of DraftCanvas  state as add primitive.
        /// </summary>
        /// <param name="primitiveName">Primitive's name.</param>
        public void AddPrimitive(string primitiveName)
        {
            if (_primitiveCreator != null)
            {
                _primitiveCreator.CancelCreation(this);
                _primitiveCreator = null;
            }
            _primitiveCreator = new DcLineSegmentCreator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="primitiveName"></param>
        public void StopAddPrimitive(string primitiveName)
        {
            if (_primitiveCreator != null)
            {
                _primitiveCreator.CancelCreation(this);
                _primitiveCreator = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visualObject"></param>
        public void RemoveVisualObject(IVisualObject visualObject)
        {
            DrawingVisual visual = GetDrawingVisualById(visualObject.ID);
            if (visual == null) return;

            RemoveVisualChild(visual);
            if (visualObject is DcLineSegment line)
            {
                PointManager.RemovePrimitivePoints(PointCollection, line);
                DcLineSegments.Remove(line);
            }
            _visualsCollection.Remove(visual);
        }

        /// <summary>
        /// Removes all visual objects from the Canvas.
        /// </summary>
        public void Clear()
        {
            foreach (var item in _visualsCollection)
            {
                RemoveVisualChild(item);
            }
            DcLineSegments.Clear();
            PointCollection.Clear();
            CanvasCounter.ResetCounter();
            _visualsCollection.Clear();
        }

        /// <summary>
        /// Finds out a visual element in the visual collection by the desired ID.
        /// </summary>
        /// <param name="visualId">The visual element Id.</param>
        /// <returns>Returns the visual element or Null if the element is not found.</returns>
        public DrawingVisualEx GetDrawingVisualById(int visualId)
        {
            return (DrawingVisualEx)_visualsCollection.Find(v => ((DrawingVisualEx)v).ID == visualId);
        }

        #endregion

        #region Overrided Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Focus();
            if (_primitiveCreator == null) _leftmouse.OnMouseDown(e.GetPosition(this), this);
            else _primitiveCreator = _primitiveCreator?.Create(e.GetPosition(this), this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_primitiveCreator == null) _leftmouse.OnMouseMove(e.GetPosition(this), this);
            else _primitiveCreator?.DrawFantom(e.GetPosition(this), this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            if (_primitiveCreator == null) _leftmouse.OnMouseUp(this);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override int VisualChildrenCount
        {
            get { return _visualsCollection.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index)
        {
            return _visualsCollection[index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dc"></param>
        protected override void OnRender(DrawingContext dc)
        {
            Brush background = this.Background;
            if (background == null)
                return;
            if (background.CanFreeze)
                background.Freeze();
            Size renderSize = this.RenderSize;
            dc.DrawRectangle(background, null, new Rect(0.0, 0.0, renderSize.Width, renderSize.Height));
        }
        #endregion
    }
}
