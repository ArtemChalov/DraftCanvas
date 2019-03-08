using System.Windows.Media;

namespace DraftCanvas
{
    static class CanvasParam
    {
        static public double Thikness { get; set; } = 1.5;

        static public double Scale { get; set; } = 1;

        static public Brush PenColor { get; set; } = Brushes.White;

        static public double CanvasWidth { get; set; }
        static public double CanvasHeight { get; set; }
    }
}
