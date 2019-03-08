using System.Windows;

namespace DraftCanvas
{
    public struct DcPoint
    {
        private readonly int _pointHash;

        public DcPoint(double x, double y, int pointHash, int ownerId)
        {
            _pointHash = pointHash;
            X = x;
            Y = y;
            IssuerHash = 0;
            SubHash = 0;
        }

        public DcPoint(Point point, int pointHash, int ownerId)
        {
            _pointHash = pointHash;
            X = point.X;
            Y = point.Y;
            IssuerHash = 0;
            SubHash = 0;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public int IssuerHash { get; set; }
        public int SubHash { get; set; }

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
