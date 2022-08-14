using Grombcross.Models;
using Grombcross.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Grombcross.Views {
    public partial class PuzzleSelectView : UserControl {
        PuzzleSelectViewModel _puzzleSelectViewModel;
        public PuzzleSelectView() {
            Loaded += OnLoaded;

            InitializeComponent();
        }
        private void OnLoaded(object sender, RoutedEventArgs e) { _puzzleSelectViewModel = (PuzzleSelectViewModel)DataContext; }

        private void PuzzleSelected(object sender, RoutedEventArgs e) {
            Button button = (Button)e.Source;
            Puzzle puzzle = (Puzzle)button.DataContext;
            _puzzleSelectViewModel.ShowGameView(puzzle.Index);
        }

        private void ShowTitle(object sender, RoutedEventArgs e) => _puzzleSelectViewModel.ShowTitleView();
        private void ShowCredits(object sender, RoutedEventArgs e) => _puzzleSelectViewModel.ShowCreditsView();
        private void ShowSettings(object sender, RoutedEventArgs e) => _puzzleSelectViewModel.ShowSettingsView();
    }
}
