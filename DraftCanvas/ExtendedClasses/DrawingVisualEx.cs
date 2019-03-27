using System.Windows.Media;

namespace DraftCanvas
{
    /// <summary>
    /// Class that extends the DrawingVisual class by the additional properties
    /// like as ID, Tag, IsDirty and IVisualObject refarence.
    /// </summary>
    public sealed class DrawingVisualEx : DrawingVisual
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

        /// <summary>
        /// 
        /// </summary>
        public bool IsSelected
        {
            get { return _visualObj.IsSelected; }
            set { _visualObj.IsSelected = value; }
        }
    }
}
