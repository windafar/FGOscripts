using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FGOscript
{
    //定义值转换器
    [ValueConversion(typeof(Bitmap), typeof(BitmapImage))]
    public class BitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
           return OptBase.OptBaseY.BitmapToBitmapImage((Bitmap)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    //定义值转换器
    [ValueConversion(typeof(bool), typeof(System.Windows.Media.Brush))]
    public class BooleanToBushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool ivalue = value!=null?(bool)value:false;
            if (ivalue)
            {
                return System.Windows.Media.Brushes.DarkGreen;
            }
            else {
                return System.Windows.Media.Brushes.DarkRed;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
