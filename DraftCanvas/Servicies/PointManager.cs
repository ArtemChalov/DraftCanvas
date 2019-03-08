using System.Collections.Generic;
using System.Linq;

namespace DraftCanvas.Servicies
{
    internal class PointManager
    {
        internal static bool HasIssuer(Canvas canvas, int pointHash)
        {
            return canvas.PointCollection[pointHash].IssuerHash != 0;
        }

        internal static bool HasSub(Canvas canvas, int pointHash)
        {
            return canvas.PointCollection[pointHash].SubHash != 0;
        }

        internal static void AddPrimitive(IPrimitive primitive, IDictionary<int, DcPoint> pointCollection)
        {
            foreach (var item in primitive.Points)
            {
                DcPoint newPoint = new DcPoint(item.Value, item.Key, primitive.ID);
                if (pointCollection.Values.Contains(newPoint))
                {
                    DcPoint pointIssuer = pointCollection.Values.Where(v => v.X == newPoint.X && v.Y == newPoint.Y).First();
                    pointIssuer.SubHash = newPoint.PointHash;
                    pointCollection[pointIssuer.PointHash] = pointIssuer;

                    newPoint.IssuerHash = pointIssuer.PointHash;
                }
                pointCollection.Add(item.Key, newPoint);
            }
        }

        internal static bool ResolveConstraint(Canvas canvas, double newX, double newY, int issuerHash)
        {
            DcPoint issuerPoint = canvas.PointCollection[issuerHash];
            DcPoint subPoint = canvas.PointCollection[issuerPoint.SubHash];

            IVisualObject visualObject = canvas.GetDrawingVisualById(subPoint.OwnerID).VisualObject;

            if (visualObject is IPrimitive primitive)
            {
                primitive.SetPoint(newX, newY, subPoint.PointHash);
            }

            return false;
        }
    }
}
