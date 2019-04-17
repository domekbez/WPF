using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
namespace laby
{
    class KonwerterDat: IValueConverter
    {
        public object Convert(object value, Type targetType,object parameter, CultureInfo culture)
        {
            if (!(value is  DateTime))
                return  null;
            DateTime v = (DateTime)value;
            string a = v.Day.ToString() + "." + v.Month.ToString() + "." + v.Year.ToString();
            return a;
        }
        public object ConvertBack(object value, Type targetType,object parameter, CultureInfo culture)
        {
            throw  new NotImplementedException();
        }
    }
}