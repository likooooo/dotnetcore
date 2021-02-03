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
using System.Collections.ObjectModel;

//https://docs.microsoft.com/zh-cn/dotnet/api/system.windows.datatemplate?redirectedfrom=MSDN&view=net-5.0
namespace Sample
{
    public struct Photo
    {
        public string Source{get;set;}
    }
    public class MyPhotos : ObservableCollection<Photo>
    {
        public MyPhotos()
        {
            string dir = @"C:\Users\like\Desktop\Project\dotnet-core\dotnetcore\ImageProcess\Sample\";
            Add(new Photo{Source = dir + "570_544_24.bmp"});
            Add(new Photo{Source = dir + "637_475_1.bmp"});
            Add(new Photo{Source = dir + "637_475_8.bmp"});
            Add(new Photo{Source = dir + "637_475_16.bmp"});
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            listBox1.ItemsSource = (new MyPhotos()).ToList<Photo>();
            // var v = myPhotos;
        }
    }
}
