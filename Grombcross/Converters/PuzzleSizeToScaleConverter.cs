using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Grombcross.Converters {
    [ValueConversion(typeof(int), typeof(int))]
    public class PuzzleSizeToScaleConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int puzzleSize = (int)value;
            double scale = 26.0384995 * Math.Pow(puzzleSize, -0.814332125);

            double appHeight = Application.Current.MainWindow.ActualHeight;
            scale *= appHeight / 800;

            return scale;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
