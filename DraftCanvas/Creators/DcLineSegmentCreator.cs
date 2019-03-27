using DraftCanvas.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DraftCanvas.Creators
{
    /// <summary>
    /// Creats a new DcLineSegment.
    /// </summary>
    public class DcLineSegmentCreator : IPrimitiveCreator
    {
        private int _pointCounter = 1;
        private Point _firstPoint;
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
                canvas.DcLineSegments.Add(new Primitives.DcLineSegment(_firstPoint.X, _firstPoint.Y, currentPoint.X, canvas.Height - currentPoint.Y));
            }
            return null;
        }
    }
}
