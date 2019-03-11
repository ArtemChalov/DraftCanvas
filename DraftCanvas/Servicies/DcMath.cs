using DraftCanvas.Primitives;
using System;

namespace DraftCanvas.Servicies
{
    /// <summary>
    /// 
    /// </summary>
    public static class DcMath
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        static public double GetDistance(double x1, double y1, double x2, double y2)
        {
            double deltaX = Math.Abs(x2 - x1);
            double deltaY = Math.Abs(y2 - y1);

            return Math.Round(Math.Sqrt((deltaX * deltaX + deltaY * deltaY)), 6);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        static public double Xoffset(double distance, double angle)
        {
            return Math.Round((distance * Math.Cos(DcMath.DegreeToRadian(angle))), 6);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        static public double Yoffset(double distance, double angle)
        {
            return Math.Round((distance * Math.Sin(DcMath.DegreeToRadian(angle))), 6);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lineSegment"></param>
        /// <returns></returns>
        static public double GetLineSegmentAngle(DcLineSegment lineSegment)
        {
            double deltaX = lineSegment.X2 - lineSegment.X1;
            double deltaY = lineSegment.Y2 - lineSegment.Y1;

            double angle = Math.Round(RadianToDegree(Math.Atan(deltaY / deltaX)), 6);

            if (deltaX < 0)
                angle += 180;

            return angle < 0 ? angle + 360 : angle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        static public double DegreeToRadian(double degree) => Math.PI * degree / 180;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="radian"></param>
        /// <returns></returns>
        static public double RadianToDegree(double radian) => radian * 180 / Math.PI;
    }
}
