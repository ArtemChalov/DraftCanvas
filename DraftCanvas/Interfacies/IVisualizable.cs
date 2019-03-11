
namespace DraftCanvas
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVisualizable : IDirty
    {
        /// <summary>
        /// 
        /// </summary>
        Canvas Owner { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DrawingVisualEx GetVisual();
    }
}
