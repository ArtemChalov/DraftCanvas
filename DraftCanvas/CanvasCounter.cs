
namespace DraftCanvas
{
    /// <summary>
    /// The class that hold a unique id to a Canvas primitive.
    /// </summary>
    public static class CanvasCounter
    {
        private static int _primitiveID = -1;

        /// <summary>
        /// The Primitive counter.
        /// </summary>
        public static int PrimitiveID => ++_primitiveID;

        /// <summary>
        /// Returns the counter to its original state.
        /// </summary>
        public static void ResetCounter() => _primitiveID = -1;
    }
}
