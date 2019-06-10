using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace TestCutImage
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(
    IntPtr hwnd
    );
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TestImage_Loaded(object sender, RoutedEventArgs e)
        {
           IntPtr hWnd = (IntPtr)0x000A0EA2;
            IntPtr hscrdc = GetWindowDC(hWnd);
            if (hscrdc == IntPtr.Zero) return;

            var ima= ImageBasic.BasicMethodClass.GetWindowCapture(hWnd);
            testImage.Source = OptBase.OptBaseY.BitmapToBitmapImage(ima);
        }

        private void TestImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process[] ps = Process.GetProcesses();
           var hWnd= ps.First(x => x.ProcessName.IndexOf("NemuHeadless") > -1).Handle;

            var ima = ImageBasic.BasicMethodClass.GetWindowCapture(hWnd);
            testImage.Source = OptBase.OptBaseY.BitmapToBitmapImage(ima);

        }
    }
}
