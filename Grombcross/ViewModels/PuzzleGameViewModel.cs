﻿using Grombcross.Models;
using Grombcross.Models.Systems;
using Grombcross.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Grombcross.Models.PuzzleGameModel;

namespace Grombcross.ViewModels {
    public class PuzzleGameViewModel : ViewModelBase {
        private readonly PuzzleGameModel _puzzleGameModel;

        public Func<bool> ShowSelectView;

        public List<List<Block>> Blocks => _puzzleGameModel.Blocks;
        public List<HintLine> LeftHintLines => _puzzleGameModel.LeftHintLines;
        public List<HintLine> TopHintLines => _puzzleGameModel.TopHintLines;
        public int PuzzleSize => _puzzleGameModel.CurrentPuzzle.Size;
        public double PuzzleScale {
            get {
                double scale = 26.0384995 * Math.Pow(PuzzleSize, -0.814332125);

                double windowHeight = Application.Current.MainWindow.ActualHeight;
                scale *= windowHeight / 800;

                return scale;
            }
        }

        public string PuzzleName => _puzzleGameModel.CurrentPuzzle.Name;
        public Bitmap CompletedImage => _puzzleGameModel.CurrentPuzzle.FinalImage;

        public bool PuzzleSolved => _puzzleGameModel.PuzzleSolved;

        public PuzzleGameViewModel(PuzzleGameModel puzzleGameModel, Func<bool> showSelectView) {
            _puzzleGameModel = puzzleGameModel;
            ShowSelectView = () => {
                AudioSystem.PlayQuickForward();
                return showSelectView();
            };
        }

        public void LeftClickBlock(Block block) {
            switch (block.State) {
                case Block.BlockState.EMPTY: FillBlock(block); break;
                case Block.BlockState.X: ClearBlock(block); break;
                case Block.BlockState.QUESTION: FillBlock(block); break;
                case Block.BlockState.FILLED: ClearBlock(block); break;
            }

            block.OnPropertyChanged(nameof(block.State));

            RefreshLineFulfilledProperties(block);
            RefreshPuzzleSolved();
        }
        public bool DragLeftClickBlock(Block block, PuzzleGameView.FillingState curFillingState) {
            bool blockStateWasChanged = false;

            switch (curFillingState) {
                case PuzzleGameView.FillingState.FILLING:
                    if (block.State == Block.BlockState.EMPTY || block.State == Block.BlockState.QUESTION) {
                        FillBlock(block);
                        blockStateWasChanged = true;
                    }
                    break;
                case PuzzleGameView.FillingState.CLEARING:
                    if (block.State != Block.BlockState.EMPTY) {
                        ClearBlock(block);
                        blockStateWasChanged = true;
                    }
                    break;
            }

            block.OnPropertyChanged(nameof(block.State));

            RefreshLineFulfilledProperties(block);
            RefreshPuzzleSolved();

            return blockStateWasChanged;
        }

        public void RightClickBlock(Block block) {
            switch (block.State) {
                case Block.BlockState.EMPTY: XBlock(block); break;
                case Block.BlockState.X: ClearBlock(block); break;
                case Block.BlockState.QUESTION: XBlock(block); break;
                case Block.BlockState.FILLED: ClearBlock(block); break;
            }

            block.OnPropertyChanged(nameof(block.State));

            RefreshLineFulfilledProperties(block);
            RefreshPuzzleSolved();
        }
        public bool DragRightClickBlock(Block block, PuzzleGameView.FillingState curFillingState) {
            bool blockStateWasChanged = false;

            switch (curFillingState) {
                case PuzzleGameView.FillingState.FILLING:
                    if (block.State == Block.BlockState.EMPTY) {
                        XBlock(block);
                        blockStateWasChanged = true;
                    }
                    break;
                case PuzzleGameView.FillingState.CLEARING:
                    if (block.State != Block.BlockState.EMPTY) {
                        ClearBlock(block);
                        blockStateWasChanged = true;
                    }
                    break;
            }

            block.OnPropertyChanged(nameof(block.State));

            RefreshLineFulfilledProperties(block);
            RefreshPuzzleSolved();

            return blockStateWasChanged;
        }

        public void MiddleClickBlock(Block block) {
            switch (block.State) {
                case Block.BlockState.EMPTY: QuestionBlock(block); break;
                case Block.BlockState.X: break;
                case Block.BlockState.QUESTION: ClearBlock(block); break;
                case Block.BlockState.FILLED: break;
            }

            block.OnPropertyChanged(nameof(block.State));

            RefreshLineFulfilledProperties(block);
            RefreshPuzzleSolved();
        }
        public bool DragMiddleClickBlock(Block block, PuzzleGameView.FillingState curFillingState) {
            bool blockStateWasChanged = false;

            switch (curFillingState) {
                case PuzzleGameView.FillingState.FILLING:
                    if (block.State == Block.BlockState.EMPTY) {
                        QuestionBlock(block);
                        blockStateWasChanged = true;
                    }
                    break;
                case PuzzleGameView.FillingState.CLEARING:
                    if (block.State == Block.BlockState.QUESTION) {
                        ClearBlock(block);
                        blockStateWasChanged = true;
                    }
                    break;
            }

            block.OnPropertyChanged(nameof(block.State));

            RefreshLineFulfilledProperties(block);
            RefreshPuzzleSolved();

            return blockStateWasChanged;
        }

        private void FillBlock(Block block) {
            _puzzleGameModel.FillBlock(block);
            AudioSystem.PlayBlockPlace();
        }
        private void XBlock(Block block) {
            _puzzleGameModel.XBlock(block);
            AudioSystem.PlayXPlace();
        }
        private void QuestionBlock(Block block) {
            _puzzleGameModel.QuestionBlock(block);
        }
        private void ClearBlock(Block block) {
            _puzzleGameModel.ClearBlock(block);
        }

        public bool QuickFillIntersectLines(Block block) {
            return _puzzleGameModel.QuickFillIntersectLines(block);
        }
        public bool QuickXIntersectLines(Block block) {
            return _puzzleGameModel.QuickXIntersectLines(block);
        }

        private void RefreshLineFulfilledProperties(Block block) {
            _puzzleGameModel.CheckRowFulfilled(block.Row);
            HintLine rowHintLine = LeftHintLines[block.Row];
            rowHintLine.OnPropertyChanged(nameof(rowHintLine.LineFulfilled));

            _puzzleGameModel.CheckColumnFulfilled(block.Column);
            HintLine columnHintLine = TopHintLines[block.Column];
            columnHintLine.OnPropertyChanged(nameof(columnHintLine.LineFulfilled));
        }



        private void RefreshPuzzleSolved() {
            bool puzzleSolvedNow = _puzzleGameModel.CheckForPuzzleSolved();
            if (puzzleSolvedNow) {
                AudioSystem.PlayPuzzleComplete();
                OnPropertyChanged(nameof(PuzzleSolved));
            }
        }

        public void ResetPuzzle() {
            _puzzleGameModel.ResetPuzzle();
        }

        public void RefreshPuzzleScale() {
            OnPropertyChanged(nameof(PuzzleScale));
        }

        // Debugging
        public int CountNumFilledBlocks() {
            int count = 0;
            for (int r = 0; r < _puzzleGameModel.BlockGridSize; r++) {
                for (int c = 0; c < _puzzleGameModel.BlockGridSize; c++) {
                    if (Blocks[r][c].State == Block.BlockState.FILLED)
                        count++;
                }
            }

            return count;
        }
    }
}
