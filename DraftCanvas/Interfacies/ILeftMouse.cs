using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DraftCanvas.Interfacies
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILeftMouse
    {
        /// <summary>
        /// /
        /// </summary>
        /// <param name="point"></param>
        /// <param name="canvas"></param>
        void OnMouseDown(Point point, DrCanvas canvas);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="canvas"></param>
        void OnMouseMove(Point point, DrCanvas canvas);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        void OnMouseUp(DrCanvas canvas);
    }
}
