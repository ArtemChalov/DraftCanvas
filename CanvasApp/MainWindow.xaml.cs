using DraftCanvas;
using DraftCanvas.Primitives;
using DraftCanvas.Servicies;
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
            DcLineSegment lineSegment = new DcLineSegment(new Point(200, 300), 100, 30);
            lineSegment.AddLocalConstraint(Constraints.Heigth);

            Canva.DcLineSegments.Add(new DcLineSegment(new Point(200, 200), 100, 90));
            Canva.DcLineSegments.Add(lineSegment);

            double acuteAngle = DcMath.GetAngleByWidth(lineSegment.Width, lineSegment.Height, lineSegment.Length);

            Mess1.Text = $"T Angle: {acuteAngle}";
            Mess2.Text = $"Angle: {lineSegment.Angle}";
            Mess3.Text = $"Width: {lineSegment.Width}";
            Mess4.Text = $"Height: {lineSegment.Height}";
            Mess5.Text = $"Length: {lineSegment.Length}";

            //Canva.DcLineSegments.Add(new DcLineSegment(new Point(100, 200), 100, 0));
            //Canva.DcLineSegments.Add(new DcLineSegment(new Point(200, 200), 100, 270));
            //Canva.DcLineSegments.Add(new DcLineSegment(200, 100, 100, 100));
        }

        private void Lines_Action(object sender, RoutedEventArgs e)
        {
            var someLine = Canva.DcLineSegments.Find(line => line.ID == 0);

            if (someLine == null) return;

            someLine.Length += 10;
            Canva.Update();

            Mess1.Text = $"T Angle: ";
            Mess2.Text = $"Angle: {someLine.Angle}";
            Mess3.Text = $"Width: {someLine.Width}";
            Mess4.Text = $"Height: {someLine.Height}";
            Mess5.Text = $"Length: {someLine.Length}";
        }
    }
}
