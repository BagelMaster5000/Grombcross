using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Grombcross.Converters {
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class LineSolvedToBrushColorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            bool isSolved = (bool)value;
            if (isSolved) {
                return new SolidColorBrush(Color.FromArgb(20, 255, 255, 255));
            }
            else {
                return new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
