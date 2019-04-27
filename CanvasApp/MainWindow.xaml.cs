using DraftCanvas;
using DraftCanvas.Primitives;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CanvasApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private DraftCanvas.DrCanvas _canvas;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _canvas = new DraftCanvas.DrCanvas(800, 600) { Background = Brushes.LightSteelBlue };
        }

        public DrCanvas Canva => _canvas;

        public Grid EditPanel { get; set; }


        private void Stop_AddPrimitive(object sender, RoutedEventArgs e)
        {
            Canva.StopAddPrimitive();
            EditPanel = null;
            OnPropertyChanged(nameof(EditPanel));
        }


        private void Add_Primitive(object sender, RoutedEventArgs e)
        {
            EditPanel = null;
            EditPanel = Canva.AddPrimitive(((FrameworkElement)sender).Tag.ToString());
            OnPropertyChanged(nameof(EditPanel));
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                _canvas.DelSelectedPrimitive();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
