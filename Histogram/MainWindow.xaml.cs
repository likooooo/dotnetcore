using System;
using System.Collections.Generic;
using System.IO;
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

namespace Histogram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ImageBrush ib = new ImageBrush();
            ib.Stretch = Stretch.None;
            string backgroundFilePath = "../ImageProcess/Sample/570_554_24.bmp";
            if(File.Exists(backgroundFilePath))
            {              
                ib.ImageSource = new System.Windows.Media.Imaging.BitmapImage(new Uri(backgroundFilePath, UriKind.RelativeOrAbsolute));
            }
            else
            {
                //throw new Exception("asd");
            }
            this.Background = ib;
            this.KeyDown += EscToExistForm;
        }
        public MainWindow(int widht,int height,string title):this()
        {
            this.Width = widht;
            this.Height = height;
            this.Title = title;
        }

        private void EscToExistForm(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
