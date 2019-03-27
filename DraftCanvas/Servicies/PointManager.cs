using System.Collections.Generic;
using System.Linq;

namespace DraftCanvas.Servicies
{
    internal class PointManager
    {
        internal static void AddPrimitivePoints(IDictionary<int, DcPoint> pointCollection, IPrimitive primitive)
        {
            foreach (var item in primitive.Points)
            {
                DcPoint newPoint = new DcPoint(item.Value, item.Key);

                newPoint = SetConstraint(newPoint, pointCollection);

                pointCollection.Add(item.Key, newPoint);
            }
        }

        internal static void RemovePrimitivePoints(IDictionary<int, DcPoint> pointCollection, IPrimitive primitive)
        {
            foreach (var item in primitive.Points)
                pointCollection.Remove(item.Key);
        }

        internal static DcPoint SetConstraint(DcPoint newPoint, IDictionary<int, DcPoint> pointCollection)
        {
            if (!pointCollection.Values.Contains(newPoint)) return newPoint;

            DcPoint pointIssuer = pointCollection.Values.Where(v => v.Equals(newPoint)).First();

            // Setting reference to the dependent point
            pointIssuer.DependedHash = newPoint.GetHashCode();
            pointCollection[pointIssuer.GetHashCode()] = pointIssuer;

            // Setting reference to the active point
            newPoint.ActiveHash = pointIssuer.GetHashCode();
            return newPoint;
        }

        internal static bool ResolveConstraint(DrCanvas canvas, double newX, double newY, int issuerHash)
        {
            DcPoint issuerPoint = canvas.PointCollection[issuerHash];
            DcPoint subPoint = canvas.PointCollection[issuerPoint.DependedHash];

            IVisualObject visualObject = canvas.GetDrawingVisualById(PointHash.GetIdFromHash(subPoint.GetHashCode()))?.VisualObject;

            if (visualObject is IPrimitive primitive)
                return primitive.SetPoint(newX, newY, subPoint.GetHashCode());

            return false;
        }
    }
}
