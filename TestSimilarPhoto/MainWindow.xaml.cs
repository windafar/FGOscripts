using System;
using System.Collections.Generic;
using System.Drawing;
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
using SimilarPhoto;

namespace TestSimilarPhoto
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Bitmap bit1 = new Bitmap(@"Model.png");
            Bitmap bit2 = new Bitmap(@"Test.png");

        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SimilarPhoto.SimilarPhoto similarPhoto1 = new SimilarPhoto.SimilarPhoto(@"Model.png");
            SimilarPhoto.SimilarPhoto similarPhoto2 = new SimilarPhoto.SimilarPhoto(@"Test.png");
            string srcstring = similarPhoto1.GetHash();
            string detstring = similarPhoto2.GetHash();
            var res= SimilarPhoto.SimilarPhoto.CalcSimilarDegree(srcstring, detstring);
        }
    }
}
