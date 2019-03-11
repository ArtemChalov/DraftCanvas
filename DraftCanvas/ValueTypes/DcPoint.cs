using System.Windows;

namespace DraftCanvas
{
    /// <summary>
    /// 
    /// </summary>
    public struct DcPoint
    {
        private readonly int _pointHash;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="pointHash"></param>
        public DcPoint(double x, double y, int pointHash)
        {
            _pointHash = pointHash;
            X = x;
            Y = y;
            ActiveHash = 0;
            DependedHash = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="pointHash"></param>
        public DcPoint(Point point, int pointHash)
        {
            _pointHash = pointHash;
            X = point.X;
            Y = point.Y;
            ActiveHash = 0;
            DependedHash = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        // If this point has influence on other point
        // this value is non zero
        public int ActiveHash { get; set; }

        // If this point is affected by other point
        // this value is non zero
        /// <summary>
        /// 
        /// </summary>
        public int DependedHash { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is DcPoint other)
                return other.X == this.X && other.Y == this.Y;
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this._pointHash;
        }
    }
}
