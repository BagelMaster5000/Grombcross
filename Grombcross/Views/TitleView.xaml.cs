using Grombcross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Grombcross.Views {
    /// <summary>
    /// Interaction logic for TitleView.xaml
    /// </summary>
    public partial class TitleView : UserControl {
        TitleViewModel _dataContext;

        public TitleView() {
            Loaded += OnLoaded;

            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e) {
            _dataContext = (TitleViewModel)DataContext;
        }

        private void ShowPuzzleSelect(object sender, RoutedEventArgs e) {
            _dataContext.ShowSelectView();
        }
    }
}
