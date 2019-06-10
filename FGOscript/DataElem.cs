using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using PropertyChanged;

namespace FGOscript
{
    [Serializable]
    [AddINotifyPropertyChangedInterface]// Install-Package PropertyChanged.Fody,INotifyPropertyChanged接口的AOP型实现
   public class DataElem
    {
        Bitmap monitorImage;
        Rectangle monitorPosition;
        string monitorCheck;

        Bitmap synCheckImage;
        Rectangle synCheckPosition;
        string synCheck;

        bool monitorCheckIsOk;
        private bool synCheckIsOk;

        public Bitmap MonitorImage { get => monitorImage; set => monitorImage = value; }
        public Rectangle MonitorPosition { get => monitorPosition; set => monitorPosition = value; }
        public string MonitorCheck { get => monitorCheck; set => monitorCheck = value; }
        public Bitmap SynCheckImage { get => synCheckImage; set => synCheckImage = value; }
        public Rectangle SynCheckPosition { get => synCheckPosition; set => synCheckPosition = value; }
        public string SynCheck { get => synCheck; set => synCheck = value; }
        public bool MonitorCheckIsOk { get => monitorCheckIsOk; set => monitorCheckIsOk = value; }
        public bool SynCheckIsOk { get => synCheckIsOk; set => synCheckIsOk = value; }

        public Bitmap curComparisonImage1 { set; get; }
        public Bitmap curComparisonImage2 { set; get; }


    }

    public class DataElemEx
    {
        DataElem de;
        string controlName;

        public string ControlName { get => controlName; set => controlName = value; }
        internal DataElem De { get => de; set => de = value; }
    }
    [Serializable]
    public class ConfigData//:ISerializable
    {
        ObservableCollection<DataElem> list;
        IntPtr hwnd;

        public IntPtr Hwnd { get => hwnd; set => hwnd = value; }
        public ObservableCollection<DataElem> List { get => list; set => list = value; }
        /// <summary>
        /// 先写成这样，不要手动调用，之后重写下单例的序列化
        /// </summary>
        public ConfigData() { }
        private static ConfigData _configData;
        public static ConfigData LoadComfig()
        {
            if (File.Exists("Config.bin"))
            {
                var buff = System.IO.File.ReadAllBytes("Config.bin");
                _configData = (ConfigData)OptBase.OptBaseY.DeserializeObject(buff);
            }
            else if(_configData==null)
                _configData = new ConfigData() {List=new ObservableCollection<DataElem>() };
            return _configData;
        }
        public void SaveComfig()
        {
            byte[] buff =OptBase.OptBaseY.SerializeObject(this);
            if (buff != null) System.IO.File.WriteAllBytes("Config.bin", buff);
        }
    }
}
