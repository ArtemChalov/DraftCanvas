
namespace DraftCanvas
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVisualObject : IVisualizable
    {
        /// <summary>
        /// 
        /// </summary>
        int ID { get; }
        /// <summary>
        /// 
        /// </summary>
        string Tag { get; }
    }
}
