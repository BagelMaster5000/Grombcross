using Grombcross.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Grombcross.Views {
    public partial class CreditsView : UserControl {
        CreditsViewModel _creditsViewModel;
        public CreditsView() {
            Loaded += OnLoaded;

            InitializeComponent();
        }
        private void OnLoaded(object sender, RoutedEventArgs e) { _creditsViewModel = (CreditsViewModel)DataContext; }

        private void ShowPuzzleSelect(object sender, RoutedEventArgs e) {
            _creditsViewModel.ShowSelectView();
        }
    }
}
