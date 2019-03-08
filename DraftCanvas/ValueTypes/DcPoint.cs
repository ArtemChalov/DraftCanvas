using System.Windows;

namespace DraftCanvas
{
    public struct DcPoint
    {
        private static int _idCounter = -1;

        public DcPoint(double x, double y, int pointHash, int ownerId)
        {
            ID = ++_idCounter;
            X = x;
            Y = y;
            PointHash = pointHash;
            OwnerID = ownerId;
            IssuerHash = 0;
            SubHash = 0;
        }

        public DcPoint(Point point, int pointHash, int ownerId)
        {
            ID = ++_idCounter;
            X = point.X;
            Y = point.Y;
            PointHash = pointHash;
            OwnerID = ownerId;
            IssuerHash = 0;
            SubHash = 0;
        }

        public int ID { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public int PointHash { get; set; }
        public int OwnerID { get; set; }
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
            return this.PointHash;
        }
    }
}
