using Grombcross.Models;
using Grombcross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

            GenerateDividingLines();
        }

        private void GenerateDividingLines() {
            int blockInterval = 5;
            int numLines = _dataContext.PuzzleSize / blockInterval;
            int lineLength = _dataContext.PuzzleSize * 11;
            SolidColorBrush lineBrush = new SolidColorBrush(Color.FromRgb(92, 183, 196));
            for (int l = 0; l < numLines - 1; l++) {
                Line verticalLine = new Line() {
                    Stroke = lineBrush,
                    StrokeThickness = 0.5,
                    X1 = (l + 1) * blockInterval * 11,
                    X2 = (l + 1) * blockInterval * 11,
                    Y1 = 0,
                    Y2 = lineLength,
                };
                Grid.SetRow(verticalLine, 1);
                Grid.SetColumn(verticalLine, 1);
                PuzzleGrid.Children.Add(verticalLine);

                Line horizontalLine = new Line() {
                    Stroke = lineBrush,
                    StrokeThickness = 0.5,
                    X1 = 0,
                    X2 = lineLength,
                    Y1 = (l + 1) * blockInterval * 11,
                    Y2 = (l + 1) * blockInterval * 11,
                };
                Grid.SetRow(horizontalLine, 1);
                Grid.SetColumn(horizontalLine, 1);
                PuzzleGrid.Children.Add(horizontalLine);
            }
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
