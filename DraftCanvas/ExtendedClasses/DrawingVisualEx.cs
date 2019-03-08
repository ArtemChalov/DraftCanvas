using DraftCanvas.Primitives;
using System.Windows.Media;

namespace DraftCanvas
{
    /// <summary>
    /// This one class extends DrawingVisual class with the ID, Tag, IsDirty and IVisualObject reference properties.
    /// The extention helps find out the visual object with tag or id in the visual collection.
    /// </summary>
    public sealed class DrawingVisualEx : DrawingVisual, IDirty
    {
        IVisualObject _visualObj;

        public DrawingVisualEx(IVisualObject primitive) : base()
        {
            _visualObj = primitive;
        }

        public IVisualObject VisualObject => _visualObj;
        public int ID => _visualObj.ID;
        public string Tag => _visualObj.Tag;

        public bool IsDirty
        {
            get { return _visualObj.IsDirty; }
            set { _visualObj.IsDirty = value; }
        }
    }
}
