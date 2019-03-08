
namespace DraftCanvas
{
    public interface IVisualObject : IVisualizable
    {
        int ID { get; }
        string Tag { get; }
    }
}
