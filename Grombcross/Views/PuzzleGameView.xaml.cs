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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Grombcross.Views {
    /// <summary>
    /// Interaction logic for PuzzleGameView.xaml
    /// </summary>
    public partial class PuzzleGameView : UserControl {
        private PuzzleGameViewModel _dataContext;

        private DoubleAnimation _blockPlaceScaleXAnimation;
        private DoubleAnimation _blockPlaceScaleYAnimation;
        private Storyboard _blockPlaceStoryboard;

        private DoubleAnimation _blockClearScaleXAnimation;
        private DoubleAnimation _blockClearScaleYAnimation;
        private Storyboard _blockClearStoryboard;

        public PuzzleGameView() {
            Loaded += OnLoaded;

            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs) {
            _dataContext = DataContext as PuzzleGameViewModel;

            GenerateDividingLines();

            InitializeAnimations();
        }

        private void InitializeAnimations() {
            _blockPlaceScaleXAnimation = new DoubleAnimation {
                From = 0.9,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            _blockPlaceScaleXAnimation.EasingFunction = new BackEase() { EasingMode = EasingMode.EaseOut };
            _blockPlaceScaleYAnimation = new DoubleAnimation {
                From = 0.9,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            _blockPlaceScaleYAnimation.EasingFunction = new BackEase() { EasingMode = EasingMode.EaseOut };

            _blockPlaceStoryboard = new Storyboard();
            _blockPlaceStoryboard.Children.Add(_blockPlaceScaleXAnimation);
            _blockPlaceStoryboard.Children.Add(_blockPlaceScaleYAnimation);


            _blockClearScaleXAnimation = new DoubleAnimation {
                To = 1.08,
                Duration = TimeSpan.FromSeconds(0.08)
            };
            _blockClearScaleXAnimation.EasingFunction = new QuarticEase() { EasingMode = EasingMode.EaseOut };
            _blockClearScaleXAnimation.AutoReverse = true;
            _blockClearScaleYAnimation = new DoubleAnimation {
                To = 1.08,
                Duration = TimeSpan.FromSeconds(0.08)
            };
            _blockClearScaleYAnimation.EasingFunction = new QuarticEase() { EasingMode = EasingMode.EaseOut };
            _blockClearScaleYAnimation.AutoReverse = true;

            _blockClearStoryboard = new Storyboard();
            _blockClearStoryboard.Children.Add(_blockClearScaleXAnimation);
            _blockClearStoryboard.Children.Add(_blockClearScaleYAnimation);
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

            CurFillingState = block.State == Block.BlockState.EMPTY ? FillingState.FILLING : FillingState.CLEARING;
            _dataContext.LeftClickBlock(block);
            CurSelectingBlock = block;

            if (block.State == Block.BlockState.EMPTY) {
                PlayBlockClearAnimation(button);
            }
            else {
                PlayBlockPlaceAnimation(button);
            }
        }
        private void PlayBlockPlaceAnimation(Button button) {
            Storyboard.SetTarget(_blockPlaceScaleXAnimation, button);
            Storyboard.SetTargetProperty(_blockPlaceScaleXAnimation, new PropertyPath("(RenderTransform).(ScaleTransform.ScaleX)"));
            Storyboard.SetTarget(_blockPlaceScaleYAnimation, button);
            Storyboard.SetTargetProperty(_blockPlaceScaleYAnimation, new PropertyPath("(RenderTransform).(ScaleTransform.ScaleY)"));
            _blockPlaceStoryboard.Begin();
        }

        private void RightOrMiddleClickBlock(object sender, MouseButtonEventArgs e) {
            Button button = sender as Button;
            if (button == null) return;

            Block block = button.DataContext as Block;
            if (block == null) return;

            if (e.ChangedButton == MouseButton.Right) {
                CurFillingState = block.State == Block.BlockState.EMPTY ? FillingState.FILLING : FillingState.CLEARING;
                _dataContext.RightClickBlock(block);
                CurSelectingBlock = block;
            }
            else {
                _dataContext.MiddleClickBlock(block);
            }

            if (block.State == Block.BlockState.EMPTY) {
                PlayBlockClearAnimation(button);
            }
            else {
                PlayBlockPlaceAnimation(button);
            }
        }
        private void PlayBlockClearAnimation(Button button) {
            Storyboard.SetTarget(_blockClearScaleXAnimation, button);
            Storyboard.SetTargetProperty(_blockClearScaleXAnimation, new PropertyPath("(RenderTransform).(ScaleTransform.ScaleX)"));
            Storyboard.SetTarget(_blockClearScaleYAnimation, button);
            Storyboard.SetTargetProperty(_blockClearScaleYAnimation, new PropertyPath("(RenderTransform).(ScaleTransform.ScaleY)"));
            _blockClearStoryboard.Begin();
        }

        private void ShowPuzzleSelect(object sender, RoutedEventArgs e) {
            _dataContext.ShowSelectView();
        }

        private void ClickedResetPuzzle(object sender, RoutedEventArgs e) {
            _dataContext.ResetPuzzle();
        }

        Block _curSelectingBlock = null;
        Block CurSelectingBlock {
            get { return _curSelectingBlock; }
            set {
                _curSelectingBlock = value;
            }
        }
        public enum FillingState { NONE, FILLING, CLEARING }
        FillingState _curFillingState = FillingState.NONE;
        FillingState CurFillingState {
            get { return _curFillingState; }
            set { _curFillingState = value; }
        }
        private void MouseMoveOverButton(object sender, MouseEventArgs e) {
            Button button = sender as Button;
            if (button == null) return;

            Block block = button.DataContext as Block;
            if (block == null || block == CurSelectingBlock) return;

            if (e.LeftButton == MouseButtonState.Pressed) {
                bool blockStateWasChanged = _dataContext.DragLeftClickBlock(block, CurFillingState);
                CurSelectingBlock = block;

                if (blockStateWasChanged) {
                    if (block.State == Block.BlockState.EMPTY) {
                        PlayBlockClearAnimation(button);
                    }
                    else {
                        PlayBlockPlaceAnimation(button);
                    }
                }
            }
            else if (e.RightButton == MouseButtonState.Pressed) {
                bool blockStateWasChanged = _dataContext.DragRightClickBlock(block, CurFillingState);
                CurSelectingBlock = block;

                if (blockStateWasChanged) {
                    if (block.State == Block.BlockState.EMPTY) {
                        PlayBlockClearAnimation(button);
                    }
                    else {
                        PlayBlockPlaceAnimation(button);
                    }
                }
            }
        }

        private void MouseReleaseFromButton(object sender, MouseButtonEventArgs e) {
            CurFillingState = FillingState.NONE;
            CurSelectingBlock = null;
        }
    }
}
