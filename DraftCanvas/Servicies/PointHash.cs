
namespace DraftCanvas.Servicies
{
    internal static class PointHash
    {
        private static readonly int _offset = 18;

        internal static int GetHashCode(int index, int id)
        {
            return (index << _offset) + id;
        }

        internal static int GetIndexFromHash(int hash)
        {
            return (hash >> _offset);
        }
    }
}
