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
            DcLineSegment lineSegment = new DcLineSegment(200, 100, 100, 200);
            lineSegment.AddLocalConstraint(Constraints.Heigth);

            //Canva.DcLineSegments.Add(new DcLineSegment(200, 200, 200, 300));
            //Canva.DcLineSegments.Add(new DcLineSegment(new Point(300, 100), 400, 90));
            Canva.DcLineSegments.Add(lineSegment);

            Mess1.Text = $"Length: {lineSegment.Length}";
            Mess2.Text = $"Angle: {lineSegment.Angle}";
            Mess3.Text = $"dX: {lineSegment.Width}";
            Mess4.Text = $"dY: {lineSegment.Height}";
            Mess5.Text = $"X1: {lineSegment.X1}";
            Mess6.Text = $"Y1: {lineSegment.Y1}";
            Mess7.Text = $"X1: {lineSegment.X2}";
            Mess8.Text = $"Y2: {lineSegment.Y2}";

            //Mess9.Text = $"T Angle: {tAngle}";
            Mess10.Text = $"Angle: {lineSegment.Angle}";

            //Canva.DcLineSegments.Add(new DcLineSegment(new Point(100, 200), 100, 0));
            //Canva.DcLineSegments.Add(new DcLineSegment(new Point(200, 200), 100, 270));
            //Canva.DcLineSegments.Add(new DcLineSegment(200, 100, 100, 100));
        }

        private void Lines_Action(object sender, RoutedEventArgs e)
        {
            var lineSegment = Canva.DcLineSegments.Find(line => line.ID == 0);

            if (lineSegment == null) return;

            lineSegment.Length += 10;
            Canva.Update();

            Mess1.Text = $"Length: {lineSegment.Length}";
            Mess2.Text = $"Angle: {lineSegment.Angle}";
            Mess3.Text = $"dX: {lineSegment.Width}";
            Mess4.Text = $"dY: {lineSegment.Height}";
            Mess5.Text = $"X1: {lineSegment.X1}";
            Mess6.Text = $"Y1: {lineSegment.Y1}";
            Mess7.Text = $"X1: {lineSegment.X2}";
            Mess8.Text = $"Y2: {lineSegment.Y2}";
        }
    }
}
