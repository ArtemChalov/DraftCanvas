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
        /// Calculates the distance between two points.
        /// </summary>
        /// <param name="x1">X coordinate of the first point.</param>
        /// <param name="y1">Y coordinate of the first point.</param>
        /// <param name="x2">X coordinate of the second point.</param>
        /// <param name="y2">Y coordinate of the second point.</param>
        /// <returns>Returns the distance between two points.</returns>
        static public double GetDistance(double x1, double y1, double x2, double y2)
        {
            double dX = Math.Abs(x2 - x1);
            double dY = Math.Abs(y2 - y1);

            return Math.Sqrt((dX * dX + dY * dY));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        static internal double Xoffset(double distance, double angle)
        {
            return distance * Math.Cos(DegreeToRadian(angle));
        }

        /// <summary>
        /// Calculate the adjacent cathetus by the angle and opposite cathetus.
        /// </summary>
        /// <param name="cathetus">Opposite cathetus.</param>
        /// <param name="angle">Angle.</param>
        /// <returns>Returns the adjacent cathetus.</returns>
        static internal double XoffsetByTan(double cathetus, double angle)
        {
            if (angle == 90 || angle == 270) return 0;
            
            return cathetus / Math.Tan(DegreeToRadian(angle));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        static internal double Yoffset(double distance, double angle)
        {
            return distance * Math.Sin(DegreeToRadian(angle));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lineSegment"></param>
        /// <returns></returns>
        static public double GetLineSegmentAngle(DcLineSegment lineSegment)
        {
            double dX = lineSegment.X2 - lineSegment.X1;
            double dY = lineSegment.Y2 - lineSegment.Y1;

            double angle = RadianToDegree(Math.Atan(dY / dX));

            if (dX < 0)
                angle += 180;

            return angle < 0 ? angle + 360 : angle;
        }

        /// <summary>
        /// Calculates a cathetus of a triangle.
        /// </summary>
        /// <param name="anotherсathetus"></param>
        /// <param name="hypotenuse"></param>
        /// <returns></returns>
        static public double GetСathetus(double anotherсathetus, double hypotenuse) => 
            Math.Sqrt(hypotenuse * hypotenuse - anotherсathetus * anotherсathetus);

        /// <summary>
        /// Calculates the rotation angle of a line.
        /// </summary>
        /// <param name="width">Width of a triangle.</param>
        /// <param name="hypotenuse">Hypotenuse of a triangle.</param>
        /// <param name="x1">X coordinate of the first point.</param>
        /// <param name="y1">Y coordinate of the first point.</param>
        /// <param name="x2">X coordinate of the second point.</param>
        /// <param name="y2">Y coordinate of the second point.</param>
        /// <returns>Returns the angle in degrees.</returns>
        static internal double GetAngleByWidth(double width, double hypotenuse, double x1, double y1, double x2, double y2)
        { // Tested
            double angle = RadianToDegree(Math.Acos(width / hypotenuse));

            double dX = x2 - x1;
            double dY = y2 - y1;

            if (dY < 0)
            {
                if (dX < 0)
                    angle = 180 + angle;
                else
                    angle = 360 - angle;
            }
            else
            {
                if (dX < 0)
                    angle = 180 - angle;
            }

            while (angle >= 360) angle -= 360;
            while (angle <= -360) angle += 360;

           return angle < 0 ? angle + 360 : angle;
        }

        /// <summary>
        /// Calculates the rotation angle of a line.
        /// </summary>
        /// <param name="height">Height of a triangle.</param>
        /// <param name="hypotenuse">Hypotenuse of a triangle.</param>
        /// <param name="x1">X coordinate of the first point.</param>
        /// <param name="y1">Y coordinate of the first point.</param>
        /// <param name="x2">X coordinate of the second point.</param>
        /// <param name="y2">Y coordinate of the second point.</param>
        /// <returns>Returns the angle in degrees.</returns>
        static internal double GetAngleByHeight(double height, double hypotenuse, double x1, double y1, double x2, double y2)
        { // Tested
            /* From -360 to 360 tested */
            double angle = RadianToDegree(Math.Asin(height / hypotenuse));

            double dX = x2 - x1;
            double dY = y2 - y1;

            if (dX < 0)
            {
                if (dY < 0)
                    angle += 180;
                else
                    angle = 180 - angle;
            }
            else
            {
                if (dY < 0)
                    angle = 360 - angle;
            }

            while (angle >= 360) angle -= 360;
            while (angle <= -360) angle += 360;

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
            return Math.Abs(y2 - y1);
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
            return Math.Abs(x2 - x1);
        }
    }
}
