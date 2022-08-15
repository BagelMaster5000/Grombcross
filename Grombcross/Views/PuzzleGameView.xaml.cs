using Grombcross.Models;
using Grombcross.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Grombcross.Views {
    public partial class PuzzleGameView : UserControl {
        private PuzzleGameViewModel _puzzleGameViewModel;

        private DoubleAnimation _blockPlaceScaleXAnimation;
        private DoubleAnimation _blockPlaceScaleYAnimation;
        private Storyboard _blockPlaceStoryboard;

        private DoubleAnimation _blockClearScaleXAnimation;
        private DoubleAnimation _blockClearScaleYAnimation;
        private Storyboard _blockClearStoryboard;

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

        #region Setup
        public PuzzleGameView() {
            Loaded += OnLoaded;

            InitializeComponent();
        }
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs) {
            _puzzleGameViewModel = DataContext as PuzzleGameViewModel;

            InitializeAnimations();

            GenerateDividingLines();
        }

        private void InitializeAnimations() {
            _blockPlaceScaleXAnimation = new DoubleAnimation {
                From = 0.2,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            _blockPlaceScaleXAnimation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
            _blockPlaceScaleYAnimation = new DoubleAnimation {
                From = 0.2,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            _blockPlaceScaleYAnimation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };

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
            int numLines = _puzzleGameViewModel.PuzzleSize / blockInterval;
            double lineLength = _puzzleGameViewModel.PuzzleSize * 11 + 0.6;
            SolidColorBrush lineBrush = new SolidColorBrush(Color.FromRgb(92, 183, 196));
            for (int l = 0; l < numLines - 1; l++) {
                Line verticalLine = new Line() {
                    Stroke = lineBrush,
                    StrokeThickness = 1,
                    X1 = (l + 1) * blockInterval * 11 + 0.3,
                    X2 = (l + 1) * blockInterval * 11 + 0.3,
                    Y1 = 0,
                    Y2 = lineLength,
                };
                Grid.SetRow(verticalLine, 1);
                Grid.SetColumn(verticalLine, 1);
                PuzzleGrid.Children.Add(verticalLine);

                Line horizontalLine = new Line() {
                    Stroke = lineBrush,
                    StrokeThickness = 1,
                    X1 = 0,
                    X2 = lineLength,
                    Y1 = (l + 1) * blockInterval * 11 + 0.3,
                    Y2 = (l + 1) * blockInterval * 11 + 0.3,
                };
                Grid.SetRow(horizontalLine, 1);
                Grid.SetColumn(horizontalLine, 1);
                PuzzleGrid.Children.Add(horizontalLine);
            }
        }
        #endregion

        // Block single-clicking
        private void ClickBlock(object sender, MouseButtonEventArgs e) {
            Button button = sender as Button;
            if (button == null) return;

            Block block = button.DataContext as Block;
            if (block == null) return;

            switch (e.ChangedButton) {
                case MouseButton.Left: LeftClickBlock(button, block); break;
                case MouseButton.Right: RightClickBlock(button, block); break;
                case MouseButton.Middle: MiddleClickBlock(button, block); break;
            }
        }
        private void LeftClickBlock(Button button, Block block) {
            CurFillingState = block.State == Block.BlockState.EMPTY ? FillingState.FILLING : FillingState.CLEARING;
            _puzzleGameViewModel.LeftClickBlock(block);
            CurSelectingBlock = block;

            DetermineBlockAnimationAndPlay(button, block);
        }
        private void RightClickBlock(Button button, Block block) {
            CurFillingState = block.State == Block.BlockState.EMPTY ? FillingState.FILLING : FillingState.CLEARING;
            _puzzleGameViewModel.RightClickBlock(block);
            CurSelectingBlock = block;

            DetermineBlockAnimationAndPlay(button, block);
        }
        private void MiddleClickBlock(Button button, Block block) {
            CurFillingState = block.State == Block.BlockState.EMPTY ? FillingState.FILLING : FillingState.CLEARING;
            _puzzleGameViewModel.MiddleClickBlock(block);
            CurSelectingBlock = block;

            DetermineBlockAnimationAndPlay(button, block);
        }


        // Block double-clicking
        private void DoubleClickBlock(object sender, MouseButtonEventArgs e) {
            Button button = sender as Button;
            if (button == null) return;

            Block block = button.DataContext as Block;
            if (block == null || block == CurSelectingBlock) return;

            if (e.ChangedButton == MouseButton.Left) {
                // TODO Try auto-fill row/column
            }
            else if (e.ChangedButton == MouseButton.Right) {
                // TODO Try auto-X row/column
            }
        }


        // Block dragging
        private void MouseMoveOverButton(object sender, MouseEventArgs e) {
            Button button = sender as Button;
            if (button == null) return;

            Block block = button.DataContext as Block;
            if (block == null || block == CurSelectingBlock) return;

            if (e.LeftButton == MouseButtonState.Pressed) {
                bool blockStateWasChanged = _puzzleGameViewModel.DragLeftClickBlock(block, CurFillingState);
                CurSelectingBlock = block;

                if (blockStateWasChanged) {
                    DetermineBlockAnimationAndPlay(button, block);
                }
            }
            else if (e.RightButton == MouseButtonState.Pressed) {
                bool blockStateWasChanged = _puzzleGameViewModel.DragRightClickBlock(block, CurFillingState);
                CurSelectingBlock = block;

                if (blockStateWasChanged) {
                    DetermineBlockAnimationAndPlay(button, block);
                }
            }
            else if (e.MiddleButton == MouseButtonState.Pressed) {
                bool blockStateWasChanged = _puzzleGameViewModel.DragMiddleClickBlock(block, CurFillingState);
                CurSelectingBlock = block;

                if (blockStateWasChanged) {
                    DetermineBlockAnimationAndPlay(button, block);
                }
            }
        }
        private void MouseReleaseFromButton(object sender, MouseButtonEventArgs e) {
            CurFillingState = FillingState.NONE;
            CurSelectingBlock = null;
        }


        // Block animations
        private void DetermineBlockAnimationAndPlay(Button button, Block block) {
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
        private void PlayBlockClearAnimation(Button button) {
            Storyboard.SetTarget(_blockClearScaleXAnimation, button);
            Storyboard.SetTargetProperty(_blockClearScaleXAnimation, new PropertyPath("(RenderTransform).(ScaleTransform.ScaleX)"));
            Storyboard.SetTarget(_blockClearScaleYAnimation, button);
            Storyboard.SetTargetProperty(_blockClearScaleYAnimation, new PropertyPath("(RenderTransform).(ScaleTransform.ScaleY)"));
            _blockClearStoryboard.Begin();
        }




        private void ShowPuzzleSelect(object sender, RoutedEventArgs e) {
            _puzzleGameViewModel.ShowSelectView();
        }

        private void ClickedResetPuzzle(object sender, RoutedEventArgs e) {
            _puzzleGameViewModel.ResetPuzzle();
        }


        private void WindowResized(object sender, SizeChangedEventArgs e) {
            _puzzleGameViewModel?.RefreshPuzzleScale();
        }
    }
}
