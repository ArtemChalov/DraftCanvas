using DraftCanvas.Primitives;
using System;

namespace DraftCanvas.Servicies
{
    public static class DcMath
    {
        static public double GetDistance(double x1, double y1, double x2, double y2)
        {
            double deltaX = Math.Abs(x2 - x1);
            double deltaY = Math.Abs(y2 - y1);

            return Math.Round(Math.Sqrt((deltaX * deltaX + deltaY * deltaY)), 6);
        }

        static public double Xoffset(double delta, double angle)
        {
            return Math.Round((delta * Math.Cos(DcMath.DegreeToRadian(angle))), 6);
        }

        static public double Yoffset(double delta, double angle)
        {
            return Math.Round((delta * Math.Sin(DcMath.DegreeToRadian(angle))), 6);
        }

        static public double GetLineSegmentAngle(DcLineSegment lineSegment)
        {
            double deltaX = lineSegment.X2 - lineSegment.X1;
            double deltaY = lineSegment.Y2 - lineSegment.Y1;

            double angle = Math.Round(RadianToDegree(Math.Atan(deltaY / deltaX)), 6);

            if (deltaX < 0)
                angle += 180;

            return angle < 0 ? angle + 360 : angle;
        }

        static public double DegreeToRadian(double degree) => Math.PI * degree / 180;

        static public double RadianToDegree(double radian) => radian * 180 / Math.PI;
    }
}
