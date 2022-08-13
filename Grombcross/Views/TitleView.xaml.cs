using Grombcross.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Grombcross.Views {
    public partial class TitleView : UserControl {
        private TitleViewModel _dataContext;
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
