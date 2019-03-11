
namespace DraftCanvas.Servicies
{
    internal static class PointHash
    {
        private static readonly int _offset = 20;
        private static readonly int _highZeroOffset = 32 - _offset;

        internal static int CreateHash(int index, int id)
        {
            return (index << _offset) + id;
        }

        internal static int GetPointIndex(int hash)
        {
            return (hash >> _offset);
        }

        internal static int GetIdFromHash(int hash)
        {
            return ((hash << _highZeroOffset) >> _highZeroOffset);
        }
    }
}
