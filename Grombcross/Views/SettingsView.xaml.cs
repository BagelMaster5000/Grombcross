using Grombcross.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Grombcross.Views {
    public partial class SettingsView : UserControl {
        SettingsViewModel _settingsViewModel;
        public SettingsView() {
            Loaded += OnLoaded;

            InitializeComponent();
        }
        private void OnLoaded(object sender, RoutedEventArgs e) { _settingsViewModel = (SettingsViewModel)DataContext; }

        private void ShowPuzzleSelect(object sender, RoutedEventArgs e) {
            _settingsViewModel.ShowSelectView();
        }

        private void ToggleMusicMute(object sender, RoutedEventArgs e) => _settingsViewModel.ToggleMusicMute();
        private void ToggleSfxMute(object sender, RoutedEventArgs e) => _settingsViewModel.ToggleSfxMute();

        private void ResetStandardPuzzlesSaveData(object sender, RoutedEventArgs e) => _settingsViewModel.ResetStandardPuzzlesSaveData();
        private void ResetBonusPuzzlesSaveData(object sender, RoutedEventArgs e) => _settingsViewModel.ResetBonusPuzzlesSaveData();
    }
}
