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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Grombcross.Views {
    /// <summary>
    /// Interaction logic for PuzzleGameView.xaml
    /// </summary>
    public partial class PuzzleGameView : UserControl {
        private PuzzleGameViewModel _dataContext;

        public PuzzleGameView() {
            Loaded += OnLoaded;

            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs) {
            _dataContext = DataContext as PuzzleGameViewModel;
        }

        private void LeftClickBlock(object sender, EventArgs e) {
            Button button = sender as Button;
            if (button == null) return;

            Block block = button.DataContext as Block;
            if (block == null) return;

            _dataContext.LeftClickBlock(block);
        }

        private void RightOrMiddleClickBlock(object sender, MouseButtonEventArgs e) {
            Button button = sender as Button;
            if (button == null) return;

            Block block = button.DataContext as Block;
            if (block == null) return;

            if (e.ChangedButton == MouseButton.Right) {
                _dataContext.RightClickBlock(block);
            }
            else {
                _dataContext.MiddleClickBlock(block);
            }
        }

        private void ShowPuzzleSelect(object sender, RoutedEventArgs e) {
            _dataContext.ShowSelectView();
        }
    }
}
