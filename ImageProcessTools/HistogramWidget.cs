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
    /// Interaction logic for HistogramWidget.xaml
    /// </summary>
    public partial class HistogramWidget : Window
    {
        //https://blog.csdn.net/chaipp0607/article/details/54427128
        public HistogramWidget()
        {
            InitializeComponent();
            this.KeyDown += EscToExistForm;
        }
        public HistogramWidget(int widht,int height,string title):this()
        {
            this.Width = widht;
            this.Height = height;
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
