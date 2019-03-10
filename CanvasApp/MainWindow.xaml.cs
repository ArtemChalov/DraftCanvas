using DraftCanvas;
using DraftCanvas.Primitives;
using System.Windows;
using System.Windows.Media;

namespace CanvasApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Canvas _canvas;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _canvas = new Canvas(800, 600) { Background = Brushes.LightSteelBlue };
        }

        public Canvas Canva => _canvas;

        private void Add_lines(object sender, RoutedEventArgs e)
        {
            Canva.DcLineSegments.Add(new DcLineSegment(100, 100, 100, 200));
            Canva.DcLineSegments.Add(new DcLineSegment(new Point(100, 200), 100, 0, LineConstraint.Angle));
            Canva.DcLineSegments.Add(new DcLineSegment(new Point(200, 200), 100, 270, LineConstraint.Angle));
            Canva.DcLineSegments.Add(new DcLineSegment(200, 100, 100, 100, LineConstraint.Angle));
        }

        private void Lines_Action(object sender, RoutedEventArgs e)
        {
            var someLine = Canva.DcLineSegments.Find(line => line.ID == 0);

            someLine.Length += 10;
            Canva.Update();
        }
    }
}
