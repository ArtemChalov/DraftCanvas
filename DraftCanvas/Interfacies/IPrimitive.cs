
using System.Collections.Generic;
using System.Windows;

namespace DraftCanvas
{
    public interface IPrimitive : IVisualObject
    {
        bool SetPoint(double newX, double newY, int pointIndex);
        IDictionary<int, Point> Points {get;}
    }
}
