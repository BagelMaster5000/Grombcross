using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Grombcross.Converters {
    [ValueConversion(typeof(Color), typeof(Color))]
    public class BrushToDarkerBrushConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Color oldColor = (Color)value;

            byte newR = (byte)(Math.Clamp(oldColor.R - 100, 0, 255));
            byte newG = (byte)(Math.Clamp(oldColor.G - 100, 0, 255));
            byte newB = (byte)(Math.Clamp(oldColor.B - 100, 0, 255));
            SolidColorBrush newBrush = new SolidColorBrush(Color.FromRgb(newR, newG, newB));
            return newBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
