using DraftCanvas.Interfacies;
using DraftCanvas.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                _pointCounter++;
                return this;
            }
            else if (_pointCounter == 2)
            {
                if (_fantom != null)
                {
                    _fantom.X2 = currentPoint.X;
                    _fantom.Y2 = canvas.Height - currentPoint.Y;
                    canvas.Update();

                    _pointCounter = 1;
                    _fantom = null;
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

                if (_fantom != null)
                {
                    _fantom.X2 = _secondPoint.X;
                    _fantom.Y2 = _secondPoint.Y;
                    canvas.Update();
                }
                else
                {
                    _fantom = new DcLineSegment(_firstPoint.X, _firstPoint.Y, _secondPoint.X, _secondPoint.Y);
                    canvas.AddToVisualCollection(_fantom);
                }
            }
        }
    }
}
