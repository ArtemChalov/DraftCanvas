
namespace DraftCanvas
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVisualObject : IVisualizable
    {
        /// <summary>
        /// The unique identifier of the visual object.
        /// </summary>
        int ID { get; }
        /// <summary>
        /// The tag of the specific visual object.
        /// </summary>
        string Tag { get; }
        /// <summary>
        /// A flag that indicates whether the object should be redrawn.
        /// </summary>
        bool IsDirty { get; set; }
        /// <summary>
        /// A flag that indicates whether an object is selected or not.
        /// </summary>
        bool IsSelected { get; set; }
    }
}
