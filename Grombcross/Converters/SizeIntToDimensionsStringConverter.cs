using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Grombcross.Converters {
    [ValueConversion(typeof(int), typeof(string))]
    public class SizeIntToDimensionsStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int size = (int)value;
            string dimensionsString = size + "x" + size;
            return dimensionsString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
