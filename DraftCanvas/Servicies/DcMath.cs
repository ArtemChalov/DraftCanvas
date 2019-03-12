using DraftCanvas.Primitives;
using System;
using System.Runtime.CompilerServices;

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
        /// Calculates the acute angle of a triangle.
        /// </summary>
        /// <param name="height">Height of a triangle.</param>
        /// <param name="width">Width of a triangle.</param>
        /// <param name="hypotenuse">Hypotenuse of a triangle.</param>
        /// <returns></returns>
        static public double GetAngleByHeight(double height, double width, double hypotenuse)
        {
            double angle = Math.Round(RadianToDegree(Math.Asin(height / hypotenuse)), 6);

            if (width < 0)
                angle = 180 - angle;

            return angle < 0 ? angle + 360 : angle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public double DegreeToRadian(double degree) => Math.PI * degree / 180;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="radian"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static public double RadianToDegree(double radian) => radian * 180 / Math.PI;

        /// <summary>
        /// Calculates a distance normalized to X axis between two points.
        /// </summary>
        /// <param name="y1">A Y coordinate of first point.</param>
        /// <param name="y2">A Y coordinate of second point.</param>
        /// <returns>Returns distance normalized to X axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetHeight(double y1, double y2)
        {
            return y2 - y1;
        }

        /// <summary>
        /// Calculates a distance normalized to Y axis between two points.
        /// </summary>
        /// <param name="x1">A X coordinate of first point.</param>
        /// <param name="x2">A X coordinate of second point.</param>
        /// <returns>Returns distance normalized to Y axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetWidth(double x1, double x2)
        {
            return x2 - x1;
        }
    }
}
