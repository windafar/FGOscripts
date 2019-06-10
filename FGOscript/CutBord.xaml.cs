using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace FGOscript
{
    /// <summary>
    /// CutBord.xaml 的交互逻辑
    /// </summary>
    public partial class CutBord : Window
    {
        MouseHook mouseHook = new MouseKeyboardLibrary.MouseHook();
        public Rect rect = new Rect();
        public object TagData;
        public CutBord()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (mouseHook.IsStarted)
                mouseHook.Stop();

            //MessageBox.Show("start");//测试代码是否被优化掉了
            Thread.Sleep(300);//这个间隔将给予windows绑定钩子的准备时间（大概是接受消息的时间）
            mouseHook.MouseMove += MouseHook_MouseMove;

            mouseHook.MouseDown += MouseHook_MouseDown;
            mouseHook.MouseUp += MouseHook_MouseUp;
        }

        private void MouseHook_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (rect.X == 0) return;
            rect.Width =Math.Abs(rect.X - e.X);
            rect.Height =Math.Abs(rect.Y - e.Y);
            rectangle.Width = rect.Width;
            rectangle.Height = rect.Height;

        }

        private void MouseHook_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mouseHook.Stop();
            if (rect.Width != 0)//已经截取到数据
                this.Close();
        }

        private void MouseHook_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            rect.X = e.X;
            rect.Y = e.Y;

            rectangle.Margin = new Thickness(rect.X, rect.Y, 0, 0);

        }

        private void Rectangle_Loaded(object sender, RoutedEventArgs e)
        {

            mouseHook.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(mouseHook.IsStarted)
                mouseHook.Stop();
            mouseHook.MouseDown -= MouseHook_MouseDown;
            mouseHook.MouseUp -= MouseHook_MouseUp;
            mouseHook.MouseMove -= MouseHook_MouseMove;

        }


    }
}
