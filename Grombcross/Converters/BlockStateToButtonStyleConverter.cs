using Grombcross.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Grombcross.Converters {
    [ValueConversion(typeof(Block), typeof(Style))]
    public class BlockStateToButtonStyleConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Style style = new Style(typeof(Button));
            style.BasedOn = (Style)App.Current.Resources["BouncyButtonBehavior"];
            Block.BlockState state = (Block.BlockState)value;
            switch (state) {
                case Block.BlockState.FILLED:
                    style.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Color.FromRgb(0, 0, 0))));
                    break;
                case Block.BlockState.MARKED:
                    style.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Color.FromRgb(235, 52, 52))));
                    break;
                case Block.BlockState.EMPTY:
                    style.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Color.FromRgb(255, 255, 255))));
                    break;
            }

            return style;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
