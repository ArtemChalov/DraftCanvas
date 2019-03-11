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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="primitive"></param>
        public DrawingVisualEx(IVisualObject primitive) : base()
        {
            _visualObj = primitive;
        }

        /// <summary>
        /// 
        /// </summary>
        public IVisualObject VisualObject => _visualObj;

        /// <summary>
        /// 
        /// </summary>
        public int ID => _visualObj.ID;

        /// <summary>
        /// 
        /// </summary>
        public string Tag => _visualObj.Tag;

        /// <summary>
        /// 
        /// </summary>
        public bool IsDirty
        {
            get { return _visualObj.IsDirty; }
            set { _visualObj.IsDirty = value; }
        }
    }
}
