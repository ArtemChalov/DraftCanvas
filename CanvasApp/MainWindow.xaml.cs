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
        DraftCanvas.DrCanvas _canvas;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _canvas = new DraftCanvas.DrCanvas(800, 600) { Background = Brushes.LightSteelBlue };
        }

        public DrCanvas Canva => _canvas;

        private void Add_lines(object sender, RoutedEventArgs e)
        {
            Canva.AddToVisualCollection(new DcRectangle(new Point(10, 10), new Point(100, 100)));

            //Canva.DcLineSegments.Add(new DcLineSegment(100, 100, 100, 200));
            //DcLineSegment lineSegment = new DcLineSegment(100, 200, 200, 200);
            //lineSegment.AddLocalConstraint(Constraints.Angle);
            //Canva.DcLineSegments.Add(lineSegment);
            //DcLineSegment lineSegment1 = new DcLineSegment(new Point(200, 100), 100, 180);
            //lineSegment1.AddLocalConstraint(Constraints.Angle);
            //Canva.DcLineSegments.Add(lineSegment1);
            //DcLineSegment lineSegment2 = new DcLineSegment(200, 200, 200, 100);
            ////lineSegment2.AddLocalConstraint(Constraints.Angle);
            //Canva.DcLineSegments.Add(lineSegment2);

            //Mess1.Text = $"ID: {lineSegment.ID}";
            //Mess2.Text = $"Length: {lineSegment.Length}";
            //Mess3.Text = $"Angle: {lineSegment.Angle}";
            //Mess4.Text = $"dX: {lineSegment.Width}";
            //Mess5.Text = $"dY: {lineSegment.Height}";
            //Mess6.Text = $"X1: {lineSegment.X1}";
            //Mess7.Text = $"Y1: {lineSegment.Y1}";
            //Mess8.Text = $"X2: {lineSegment.X2}";
            //Mess9.Text = $"Y2: {lineSegment.Y2}";

            //Canva.DcLineSegments.Add(new DcLineSegment(new Point(100, 200), 100, 0));
            //Canva.DcLineSegments.Add(new DcLineSegment(new Point(200, 200), 100, 270));
            //Canva.DcLineSegments.Add(new DcLineSegment(200, 100, 100, 100));
        }

        private void Lines_Action(object sender, RoutedEventArgs e)
        {
            var lineSegment = (DcLineSegment)Canva.DcLineSegments.Find(line => line.ID == 0);

            if (lineSegment == null) return;

            lineSegment.Length += 10;
            //lineSegment.IsSelected = !lineSegment.IsSelected;
            Canva.Update();

            Mess1.Text = $"ID: {lineSegment.ID}";
            Mess2.Text = $"Length: {lineSegment.Length}";
            Mess3.Text = $"Angle: {lineSegment.Angle}";
            Mess4.Text = $"dX: {lineSegment.Width}";
            Mess5.Text = $"dY: {lineSegment.Height}";
            Mess6.Text = $"X1: {lineSegment.X1}";
            Mess7.Text = $"Y1: {lineSegment.Y1}";
            Mess8.Text = $"X2: {lineSegment.X2}";
            Mess9.Text = $"Y2: {lineSegment.Y2}";
        }

        private void Clear_All(object sender, RoutedEventArgs e)
        {
            //var lineSegment = Canva.DcLineSegments.Find(line => line.ID == 1);

            //if (lineSegment == null) return;

            //lineSegment.Length += 10;
            ////lineSegment.IsSelected = !lineSegment.IsSelected;
            //Canva.Update();

            Canva.Clear();
        }

        private void Add_LineSegment(object sender, RoutedEventArgs e)
        {
            Canva.AddPrimitive(((FrameworkElement)sender).Tag.ToString());
        }

        private void Stop_LineSegment(object sender, RoutedEventArgs e)
        {
            Canva.StopAddPrimitive();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                _canvas.DelSelectedPrimitive();
            }
        }
    }
}
