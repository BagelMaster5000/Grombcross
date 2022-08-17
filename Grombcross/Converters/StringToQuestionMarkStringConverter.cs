using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Grombcross.Converters {
    [ValueConversion(typeof(string), typeof(string))]
    public class StringToQuestionMarkStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string originalString = (string)value;

            string questionString = new String(originalString.Select(c => c == ' ' ? ' ' : '?').ToArray());
            return questionString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
