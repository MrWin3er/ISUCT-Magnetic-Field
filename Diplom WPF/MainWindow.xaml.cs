using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Threading;

namespace Diplom_WPF
{
    public partial class MainWindow : Window
    {
        const double r = 12.5e-2;
        const double U = 12;
        const double Hg = 12.709;

        int N = 1;
        double Current;
        double He;
        double R;


        int _step = 1;
        int _startAngle;
        Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            
            //_startAngle = random.Next(360 / _step) * _step;
            Set_Arow_Angle(_startAngle);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var rotateTransform = CompasImage.RenderTransform as RotateTransform;
            var transform = new RotateTransform(_step + (rotateTransform?.Angle ?? 0), CompasImage.Width/2,CompasImage.Height/2);
            CompasImage.RenderTransform = transform;
        }
       

        private void Set_Arow_Angle(double Angle)
        {
            double tempAngle = Angle + _startAngle;
            CompasArrow.RenderTransform = new RotateTransform(tempAngle, CompasArrow.Width / 2, CompasArrow.Height / 2);
            //var rotateTransform = CompasArrow.RenderTransform as RotateTransform;
            //var transform = new RotateTransform(Angle + (rotateTransform?.Angle ?? 0), CompasArrow.Width / 2, CompasArrow.Height / 2);
            //CompasArrow.RenderTransform = transform;
        }

        private void Count_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            N = Count.SelectedIndex + 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var rotateTransform = CompasImage.RenderTransform as RotateTransform;
            var transform = new RotateTransform(-_step + (rotateTransform?.Angle ?? 0), CompasImage.Width / 2, CompasImage.Height / 2);
            CompasImage.RenderTransform = transform;
        }

        private void Reostate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SourceON.IsChecked == true)
            {
                R = (float)Reostate.Value;
                Current = U / R;
                Amper.Text = String.Format("{0:f2}", Current);
                He = (Current * N) / (2 * r);
                if(Polar.SelectedIndex == 0) Set_Arow_Angle(Math.Atan(He / Hg) / 3.1415 * 180);
                else Set_Arow_Angle(-(Math.Atan(He / Hg) / 3.1415 * 180));

            }
        }

        private void SourceOFF_Checked(object sender, RoutedEventArgs e)
        {
            Set_Arow_Angle(0);
            Polar.IsEnabled = true;
            Count.IsEnabled = true;
            Amper.Text = "0";
        }

        private void SourceON_Checked(object sender, RoutedEventArgs e)
        {
            Polar.IsEnabled = false;
            Count.IsEnabled = false;
        }
    }
}
