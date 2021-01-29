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
    /// Interaction logic for ImageWidget.xaml
    /// </summary>
    public partial class ImageWidget : Window
    {
        //https://blog.csdn.net/chaipp0607/article/details/54427128
        public ImageWidget()
        {
            InitializeComponent();
            string backgroundFilePath = @"C:\Users\like\Desktop\Project\dotnet-core\dotnetcore\ImageProcess\Sample\570_544_24.bmp";
            imgContainer.Stretch = Stretch.Fill;
            imgContainer.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(backgroundFilePath, UriKind.RelativeOrAbsolute));
            Title = Title + this.GetHashCode().ToString();
            this.KeyDown += EscToExistForm;
        }
        public ImageWidget(int widht,int height,string title):this()
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
