using DraftCanvas;
using DraftCanvas.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            _canvas = new Canvas(800, 600) { Background = Brushes.LightGray };
        }

        public Canvas Canva => _canvas;

        private void Add_lines(object sender, RoutedEventArgs e)
        {
            Canva.AddToVisualCollection(new DcLineSegment(100, 100, 100, 200));
            Canva.AddToVisualCollection(new DcLineSegment(new Point(100, 200), 100, 0, LineConstraint.Angle));
            Canva.AddToVisualCollection(new DcLineSegment(new Point(200, 200), 100, 270, LineConstraint.Angle));
            Canva.AddToVisualCollection(new DcLineSegment(200, 100, 100, 100, LineConstraint.Angle));
        }

        private void Lines_Action(object sender, RoutedEventArgs e)
        {

        }
    }
}
