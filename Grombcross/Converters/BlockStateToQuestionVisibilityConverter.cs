using Grombcross.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Grombcross.Converters {
    [ValueConversion(typeof(Block), typeof(Visibility))]
    public class BlockStateToQuestionVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Block.BlockState state = (Block.BlockState)value;
            Visibility visibility = state == Block.BlockState.QUESTION ? Visibility.Visible : Visibility.Collapsed;

            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
