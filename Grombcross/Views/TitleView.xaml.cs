using Grombcross.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Grombcross.Views {
    public partial class TitleView : UserControl {
        private TitleViewModel _titleViewModel;
        public TitleView() {
            Loaded += OnLoaded;

            InitializeComponent();
        }
        private void OnLoaded(object sender, RoutedEventArgs e) { _titleViewModel = (TitleViewModel)DataContext; }

        private void ShowPuzzleSelect(object sender, RoutedEventArgs e) {
            _titleViewModel.ShowSelectView();
        }
    }
}
