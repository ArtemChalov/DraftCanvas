using DraftCanvas.EditPanels;
using DraftCanvas.Interfacies;
using DraftCanvas.Primitives;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace DraftCanvas.LeftMouseAction
{
    /// <summary>
    /// Creats a new DcLineSegment.
    /// </summary>
    public class DcLineSegmentCreator : IPrimitiveCreator
    {
        private int _pointCounter = 1;
        private Point _firstPoint;
        private DcLineSegment _fantom;
        private bool _created = false;

        /// <summary>
        /// 
        /// </summary>
        public DcLineSegmentCreator()
        {
            EditPanel = new DcLineSegmentPanel();
        }

        /// <summary>
        /// 
        /// </summary>
        public ContentControl EditPanel { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPoint"></param>
        /// <param name="canvas"></param>
        /// <returns></returns>
        public IPrimitiveCreator Create(Point currentPoint, DrCanvas canvas)
        {
            if (_pointCounter == 1)
            {
                _firstPoint = new Point(currentPoint.X, canvas.Height - currentPoint.Y);
                ((DcLineSegmentPanel)EditPanel).X1 = _firstPoint.X;
                ((DcLineSegmentPanel)EditPanel).Y1 = _firstPoint.Y;
                _pointCounter++;
                return this;
            }
            else if (_pointCounter == 2)
            {
                if (_fantom != null)
                {
                    _fantom.X2 = currentPoint.X;
                    _fantom.Y2 = canvas.Height - currentPoint.Y;

                    canvas.Primitives.Add(_fantom);
                    canvas.Update();

                    _created = true;
                    _pointCounter = 1;
                    ((DcLineSegmentPanel)EditPanel).ID = null;
                    ((DcLineSegmentPanel)EditPanel).X2 = null;
                    ((DcLineSegmentPanel)EditPanel).Y2 = null;
                    ((DcLineSegmentPanel)EditPanel).Length = null;
                    ((DcLineSegmentPanel)EditPanel).Angle = null;
                }
                
                return this;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPoint"></param>
        /// <param name="canvas"></param>
        /// <returns></returns>
        public void DrawFantom(Point currentPoint, DrCanvas canvas)
        {
            if (_pointCounter == 2)
            {
                Point _secondPoint = new Point(currentPoint.X, canvas.Height - currentPoint.Y);

                if (_fantom != null && !_created)
                {
                    _fantom.X2 = _secondPoint.X;
                    _fantom.Y2 = _secondPoint.Y;
                    ((DcLineSegmentPanel)EditPanel).X2 = _secondPoint.X;
                    ((DcLineSegmentPanel)EditPanel).Y2 = _secondPoint.Y;
                    ((DcLineSegmentPanel)EditPanel).Length = _fantom.Length;
                    ((DcLineSegmentPanel)EditPanel).Angle = _fantom.Angle;
                    canvas.Update();
                }
                else
                {
                    _created = false;
                    _fantom = new DcLineSegment(_firstPoint.X, _firstPoint.Y, _secondPoint.X, _secondPoint.Y);
                    ((DcLineSegmentPanel)EditPanel).ID = _fantom.ID;
                    ((DcLineSegmentPanel)EditPanel).X1 = _firstPoint.X;
                    ((DcLineSegmentPanel)EditPanel).Y1 = _firstPoint.Y;
                    canvas.AddToVisualCollection(_fantom);
                }
            }
            if (_pointCounter == 1)
            {
                ((DcLineSegmentPanel)EditPanel).X1 = currentPoint.X;
                ((DcLineSegmentPanel)EditPanel).Y1 = canvas.Height - currentPoint.Y;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        public void CancelCreation(DrCanvas canvas)
        {
            if (_pointCounter == 2)
            {
                if (_fantom != null)
                {
                    canvas.RemoveVisualObject(_fantom);
                    _fantom = null;
                }
            }
        }
    }
}
