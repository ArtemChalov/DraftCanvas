
using System.Collections.Generic;
using System.Windows;

namespace DraftCanvas
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IPrimitive : IVisualObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        /// <param name="pointIndex"></param>
        /// <returns></returns>
        bool SetPoint(double newX, double newY, int pointIndex);
        /// <summary>
        /// 
        /// </summary>
        IDictionary<int, Point> Points {get;}
    }
}
