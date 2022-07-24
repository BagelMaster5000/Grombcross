using Grombcross.Models;
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
    /// Interaction logic for PuzzleSelectView.xaml
    /// </summary>
    public partial class PuzzleSelectView : UserControl {
        PuzzleSelectViewModel _dataContext;

        public PuzzleSelectView() {
            Loaded += OnLoaded;

            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e) {
            _dataContext = (PuzzleSelectViewModel)DataContext;
            PuzzleSelectView puzzleSelectView = this;
            var grid = FindName("PuzzleGrid");
        }

        private void PuzzleSelected(object sender, RoutedEventArgs e) {
            Button button = (Button)e.Source;
            Puzzle puzzle = (Puzzle)button.DataContext;
            _dataContext.ShowGameView(puzzle.Index);
        }
    }
}
