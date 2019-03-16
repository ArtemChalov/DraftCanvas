
namespace DraftCanvas
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVisualizable
    {
        /// <summary>
        /// 
        /// </summary>
        DrCanvas Owner { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DrawingVisualEx GetVisual();
    }
}
