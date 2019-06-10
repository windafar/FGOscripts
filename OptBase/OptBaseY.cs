using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace OptBase
{
    /// <summary>
    /// 非系统工具类
    /// </summary>
    public class OptBaseY
    {
        public static BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.Seek(0, SeekOrigin.Begin);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = ms;
            bitmapImage.EndInit();
            bitmapImage.Freeze();
            return bitmapImage;
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj">将要序列化的对象</param>
        /// <param name="T">将要序列化的对的类型</param>
        /// <param name="path">xml文件存放位置(覆盖)</param>
        static public void WriteXmlSerializerObj<T>(object obj, string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            try
            {
                FileStream fs = File.Create(path);
                xs.Serialize(fs, obj);
                fs.Dispose();
            }
            catch (Exception)
            {
                throw new FieldAccessException("保存到磁盘失败，文件正在被使用");
            }

        }

        static public T LoadSerializerObjFromXml<T>(string filename)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            try
            {
                FileStream fs = File.OpenRead(filename);
                T Config = (T)xs.Deserialize(fs);
                fs.Dispose();
                return Config;
            }
            catch (Exception)
            {
                throw new FileLoadException("xml文件加载失败");
            }
            
        }

        /// <summary>
        /// 把对象序列化为字节数组
        /// </summary>
        public static byte[] SerializeObject(object obj)
        {
            if (obj == null)
                return null;
            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes, 0, bytes.Length);
            ms.Close();
            return bytes;
        }

        /// <summary>
        /// 把字节数组反序列化成对象
        /// </summary>
        public static object DeserializeObject(byte[] bytes)
        {
            object obj = null;
            if (bytes == null)
                return obj;
            MemoryStream ms = new MemoryStream(bytes);
            ms.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            obj = formatter.Deserialize(ms);
            ms.Close();
            return obj;
        }
    }
}
