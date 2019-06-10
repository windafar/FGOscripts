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

using OptBase;

namespace FGOscript
{
    /// <summary>
    /// mystrack.xaml 的交互逻辑
    /// </summary>
    public partial class mystrack : UserControl
    {

        public mystrack()
        {
            InitializeComponent();
        }

        private void MonitorImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CutBord cutBord = new CutBord();
            cutBord.Closing += CutBord_Closing;
            cutBord.Show();
            //存储当前项数据
            cutBord.TagData = new DataElemEx()
            {
                ControlName = ((FrameworkElement)sender).Name,
                De = (DataElem)((FrameworkElement)sender).DataContext
            };
        }
        private void CheckImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CutBord cutBord = new CutBord();
            cutBord.Closing += CutBord_Closing;
            cutBord.Show();
            //存储当前项数据
            cutBord.TagData = new DataElemEx()
            {
                ControlName = ((FrameworkElement)sender).Name,
                De = (DataElem)((FrameworkElement)sender).DataContext
            };
        }
        private void CutBord_Closing(object sender, EventArgs e)
        {
            ((CutBord)sender).Closing -= CutBord_Closing;
            Rect rect = ((CutBord)sender).rect;
            var bitmap = ImageBasic.BasicMethodClass.GetWindowCapture(MainWindow.configData.Hwnd);
            var dstmap= ImageBasic.BasicMethodClass.CutImage(bitmap, rect.X, rect.Y, rect.Width, rect.Height);
            if (dstmap == null) return;
            bitmap.Dispose(); bitmap = null;
             DataElemEx dex = ((CutBord)sender).TagData as DataElemEx;
            dex.De.GetType().GetProperty(dex.ControlName).SetValue(dex.De, dstmap);
            if (dex.ControlName.IndexOf("Monitor") > -1)
                dex.De.MonitorPosition = new System.Drawing.Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
            else if(dex.ControlName.IndexOf("SynCheck") > -1)
                dex.De.SynCheckPosition = new System.Drawing.Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);

        }
        

    }
}
