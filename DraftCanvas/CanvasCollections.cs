
namespace DraftCanvas
{
    public static class CanvasCollections
    {
        private static int _pointID = -1;
        private static int _primitiveID = -1;

        public static int PointId => ++_pointID;
        public static int PrimitiveID => ++_primitiveID;
    }
}
