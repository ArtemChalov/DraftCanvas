
namespace DraftCanvas
{
    public static class CanvasCounter
    {
        private static int _primitiveID = -1;

        public static int PrimitiveID => ++_primitiveID;
    }
}
