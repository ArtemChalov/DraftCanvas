using DraftCanvas.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DraftCanvas.EditPanels
{
    /// <summary>
    /// Логика взаимодействия для DcLineSegmentPanel.xaml
    /// </summary>
    public partial class DcLineSegmentPanel : UserControl, INotifyPropertyChanged
    {
        private double _X1Value;
        private double _Y1Value;
        private double? _X2Value = null;
        private double? _Y2Value = null;
        private double? _Length = null;
        private double? _Angle = null;
        private int? _id = null;

        /// <summary>
        /// 
        /// </summary>
        public DcLineSegmentPanel()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// 
        /// </summary>
        public int? ID
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public double X1
        {
            get { return _X1Value; }
            set { _X1Value = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Y1
        {
            get { return _Y1Value; }
            set { _Y1Value = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public double? X2
        {
            get { return _X2Value; }
            set { _X2Value = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public double? Y2
        {
            get { return _Y2Value; }
            set { _Y2Value = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public double? Length
        {
            get { return _Length; }
            set { _Length = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public double? Angle
        {
            get { return _Angle; }
            set { _Angle = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
