using Grombcross.Models;
using Grombcross.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace Grombcross.Views {
    public partial class PuzzleSelectView : UserControl {
        PuzzleSelectViewModel _puzzleSelectViewModel;
        public PuzzleSelectView() {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;

            InitializeComponent();
        }
        private void OnLoaded(object sender, RoutedEventArgs e) {
            _puzzleSelectViewModel = (PuzzleSelectViewModel)DataContext;
            MainViewModel.OnViewChanged += SetGridRowsAndColumns;

            SetGridRowsAndColumns();
        }
        private void OnUnloaded(object sender, RoutedEventArgs e) {
            MainViewModel.OnViewChanged -= SetGridRowsAndColumns;
        }

        private void SetGridRowsAndColumns() {
            int numPuzzles = _puzzleSelectViewModel.AllPuzzles.Count;
            int numColumns = GlobalVariables.NumPuzzleSelectColumns;
            int numRows = numPuzzles / numColumns;
            if (numPuzzles % numColumns != 0) {
                numRows++;
            }

            string xaml =
                @"<ItemsPanelTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'>" +
                    "<Grid>" +
                        "<Grid.RowDefinitions>";
            for (int r = 0; r < numRows; r++) { xaml += @"<RowDefinition Height=""190"" />"; }
            xaml +=
                        "</Grid.RowDefinitions>" +
                        "<Grid.ColumnDefinitions>";
            for (int c = 0; c < numColumns; c++) { xaml += @"<ColumnDefinition Width=""190"" />"; }
            xaml +=
                        "</Grid.ColumnDefinitions>" +
                    "</Grid>" +
                "</ItemsPanelTemplate>";

            ItemsPanelTemplate itemsPanelTemplate = XamlReader.Parse(xaml) as ItemsPanelTemplate;

            PuzzleView.ItemsPanel = itemsPanelTemplate;
        }

        private void PuzzleSelected(object sender, RoutedEventArgs e) {
            Button button = (Button)e.Source;
            Puzzle puzzle = (Puzzle)button.DataContext;
            _puzzleSelectViewModel.ShowGameView(puzzle.Index);
        }

        private void ShowTitle(object sender, RoutedEventArgs e) => _puzzleSelectViewModel.ShowTitleView();
        private void ShowCredits(object sender, RoutedEventArgs e) => _puzzleSelectViewModel.ShowCreditsView();
        private void ShowPuzzleSelect(object sender, RoutedEventArgs e) => _puzzleSelectViewModel.ShowPuzzleSelectView();
        private void ShowSettings(object sender, RoutedEventArgs e) => _puzzleSelectViewModel.ShowSettingsView();
    }
}
