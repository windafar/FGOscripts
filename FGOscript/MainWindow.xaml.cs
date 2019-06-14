using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ImageBasic;

namespace FGOscript
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        InitWindows InitWindows;
       public static ConfigData configData;
        ObservableCollection<DataElem> list = new ObservableCollection<DataElem>();
        bool isIterationThreadSuspend = true;
        Thread IterationThread;
        MouseKeyboardLibrary.KeyboardHook keyboardHook = new MouseKeyboardLibrary.KeyboardHook();
        int curIterationIndex = 0;

        public int CurIterationIndex
        {
            get {
                return curIterationIndex; }
            set
            {
                if (value < 0)
                    curIterationIndex = list.Count - 1;
                else if (value > list.Count - 1)
                    curIterationIndex = 0;
                else curIterationIndex = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            configData = ConfigData.LoadComfig();
            list = configData.List;

            keyboardHook.KeyDown += KeyboardHook_KeyDown;
            keyboardHook.Start();
        }

        private void KeyboardHook_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
                isIterationThreadSuspend = true;
        }

        private void SelectWindowsButton_Click(object sender, RoutedEventArgs e)
        {
            if (InitWindows != null) InitWindows.Close();
            InitWindows = new InitWindows();
            InitWindows.Show();
        }

        private void AddToLastButton_Click(object sender, RoutedEventArgs e)
        {
            list.Add(new DataElem()
            {
                MonitorImage = new System.Drawing.Bitmap("add.jpg"),
                MonitorCheck = "监视图",
                MonitorPosition = new System.Drawing.Rectangle(),
                SynCheckImage = new System.Drawing.Bitmap("add.jpg"),
                SynCheck = "同步区域说明",
                SynCheckPosition = new System.Drawing.Rectangle(),
            }
            );
            
        }

        private void RemoveSelectButton_Click(object sender, RoutedEventArgs e)
        {
            list.RemoveAt(RunningListBox.SelectedIndex);
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            IterationThread = new Thread((ThreadStart)delegate
            {
                while (!isIterationThreadSuspend)
                {
                    Dispatcher.Invoke((ThreadStart)delegate { this.Title = "当前比较：" + CurIterationIndex; });
                    if (!RealTimeSynCheckPositionContrast(list[CurIterationIndex]))
                    {//没有通过同步检查
                        list[CurIterationIndex].SynCheckIsOk = false;
                        CurIterationIndex++;
                        continue;
                    }
                    list[CurIterationIndex].SynCheckIsOk = true;

                    if (RealTimeMonitorPositionContrast(list[CurIterationIndex],16))
                    {//监视到期望区域
                        list[CurIterationIndex].MonitorCheckIsOk = true;
                        //MonitorCheck的格式如下：
                        //同步界面说明S：监视区域说明A：监视区域说明B
                        var mc = list[CurIterationIndex].MonitorCheck.Split(':');
                        string S = "", A = "", B = "";
                        if (mc.Length >= 1) S = mc[0];
                        if (mc.Length >= 2) A = mc[1];
                        if (mc.Length >= 3) B = mc[2];
                        switch (S)
                        {
                            case "出卡界面":
                                //通过对A,B的自定义分类完成策略
                                //需要注意的是相似比较只使用了灰度图
                                //下面的策略代码适用于fgo出卡阶段{A：英雄，B：指令卡}
                                //首先，对每个监视中队列中的牌进行测试，测试到存在的进行分组
                                //选出 
                                //list.Where(x => x.MonitorCheck.IndexOf("出卡界面") == 0);
                                //检查给出的五张指令卡是否在队列中可以对应到

                                /////////////////////////////////////////////////
                                ///////////////////////////////////////////////////
                              
                                break;
                            default://缺省则直接点击处理
                                OptBase.OptBaseX.SetMouseClick(list[CurIterationIndex].MonitorPosition);
                                break;
                        }
                    }
                    else
                    {
                        list[CurIterationIndex].MonitorCheckIsOk = false;
                    }
                    CurIterationIndex++;
                    Thread.Sleep(250);
                }
            });

            isIterationThreadSuspend = false;
            IterationThread.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            isIterationThreadSuspend = true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            configData.SaveComfig();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (InitWindows != null) InitWindows.Close();
        }

        private void RunningListBox_Loaded(object sender, RoutedEventArgs e)
        {
            RunningListBox.ItemsSource = list;
        }

        /// <summary>
        /// 监视图比较相似度
        /// </summary>
        /// <param name="dataElem">预先设定好的数据对象</param>
        /// <param name="excsd">期望相似度</param>
        /// <returns></returns>
        private bool RealTimeMonitorPositionContrast(DataElem dataElem,int excsd=12)
        {
            var bitmap = BasicMethodClass.GetWindowCapture(configData.Hwnd);
            var cutbitmap= BasicMethodClass.CutImage(bitmap, dataElem.MonitorPosition);
            bitmap.Dispose(); bitmap = null;
            int csd= BasicMethodClass.SimilarPhoto.CalcSimilarDegree(cutbitmap, dataElem.MonitorImage);
            if (dataElem.curComparisonImage1 != null) dataElem.curComparisonImage1.Dispose();
            if (dataElem.curComparisonImage2 != null) dataElem.curComparisonImage2.Dispose();
            dataElem.curComparisonImage1 = cutbitmap;
            dataElem.curComparisonImage2 = new System.Drawing.Bitmap(dataElem.SynCheckImage);
            //bitmap.Dispose();
            //cutbitmap.Dispose();
            return csd < excsd;
        }
        /// <summary>
        /// 位置检查图比较相似度
        /// </summary>
        /// <param name="dataElem">预先设定好的数据对象</param>
        /// <param name="excsd">期望相似度</param>
        /// <returns></returns>
        private bool RealTimeSynCheckPositionContrast(DataElem dataElem, int excsd = 12)
        {
            var bitmap = BasicMethodClass.GetWindowCapture(configData.Hwnd);
            var cutbitmap = BasicMethodClass.CutImage(bitmap, dataElem.SynCheckPosition);
            bitmap.Dispose(); bitmap = null;

            int csd = BasicMethodClass.SimilarPhoto.CalcSimilarDegree(cutbitmap, dataElem.SynCheckImage);
            if (dataElem.curComparisonImage1 != null)
            {
                dataElem.curComparisonImage1.Dispose();
                dataElem.curComparisonImage1 = null;
            }
            if (dataElem.curComparisonImage2 != null)
            {
                dataElem.curComparisonImage2.Dispose();
                dataElem.curComparisonImage1 = null;
            }
            dataElem.curComparisonImage1 = cutbitmap;
            dataElem.curComparisonImage2 = new System.Drawing.Bitmap(dataElem.SynCheckImage);
          //  if (bitmap!=null)
          //      bitmap.Dispose();
         //   if (cutbitmap != null)
         //       cutbitmap.Dispose();
            return csd < excsd;
        }

        private void RunningListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                list.RemoveAt(RunningListBox.SelectedIndex);
            }
        }
    }
}
