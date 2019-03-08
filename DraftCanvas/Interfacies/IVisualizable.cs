
namespace DraftCanvas
{
    public interface IVisualizable : IDirty
    {
        Canvas Owner { get; set; }
        DrawingVisualEx GetVisual();
    }
}
