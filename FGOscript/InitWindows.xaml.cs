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
using System.Windows.Shapes;
using MouseKeyboardLibrary;
using OptBase;
using System.Threading;

namespace FGOscript
{
    /// <summary>
    /// InitWindows.xaml 的交互逻辑
    /// </summary>
    public partial class InitWindows : Window
    {
        bool IsSelecting = false;
        /// <summary>
        /// 当前选择的窗口句柄
        /// </summary>

        Timer timer;
        /// <summary>
        /// 监视用触发器
        /// </summary>
        MouseHook mouseHook = new MouseKeyboardLibrary.MouseHook();
        public InitWindows()
        {
            InitializeComponent();
            mouseHook.MouseDown += MouseHook_Click;

        }
        Thread t1;
        private void MouseHook_Click(object sender, EventArgs e)
        {
            if (IsSelecting == false) return;
            mouseHook.Stop();
            IsSelecting = false;
            this.Background = Brushes.Transparent;
            t1= new Thread((ThreadStart)delegate
            {
                Thread.Sleep(300);
                MainWindow.configData.Hwnd = OptBaseX.WindowFromPoint();
                Dispatcher.BeginInvoke((ThreadStart)delegate
                {
                    OutputText.Text = "窗口名称：" + OptBaseX.GetWindowNameFromHWND(MainWindow.configData.Hwnd) + "窗口类名" + OptBaseX.GetWindowClassNameFromHWND(MainWindow.configData.Hwnd);

                    var bitmap = ImageBasic.BasicMethodClass.GetWindowCapture(MainWindow.configData.Hwnd);
                    BitmapImage bitmapImage = OptBaseY.BitmapToBitmapImage(bitmap);
                    bitmap.Dispose(); bitmap = null;
                    PreviewImage.Source = bitmapImage;


                });
            });
            t1.Start();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            PreviewImage.Source = new BitmapImage();
            mouseHook.Start();
            IsSelecting = true;
            this.Background = new SolidColorBrush(new Color() { A = 0x10, R = 0xff, B = 0xff, G = 0xff });
        }

        private void CloseWindowsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PreviewImage_Loaded(object sender, RoutedEventArgs e)
        {
            mouseHook.Start();
            IsSelecting = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mouseHook.MouseDown -= MouseHook_Click;

        }

    }
}
