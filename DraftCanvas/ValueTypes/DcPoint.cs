using System.Windows;

namespace DraftCanvas
{
    public struct DcPoint
    {
        private readonly int _pointHash;

        public DcPoint(double x, double y, int pointHash)
        {
            _pointHash = pointHash;
            X = x;
            Y = y;
            ActiveHash = 0;
            DependedHash = 0;
        }

        public DcPoint(Point point, int pointHash)
        {
            _pointHash = pointHash;
            X = point.X;
            Y = point.Y;
            ActiveHash = 0;
            DependedHash = 0;
        }

        public double X { get; set; }
        public double Y { get; set; }
        // If this point has influence on other point
        // this value is non zero
        public int ActiveHash { get; set; }
        // If this point is affected by other point
        // this value is non zero
        public int DependedHash { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is DcPoint other)
                return other.X == this.X && other.Y == this.Y;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return this._pointHash;
        }
    }
}
