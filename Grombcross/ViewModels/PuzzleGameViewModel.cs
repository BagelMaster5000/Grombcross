using Grombcross.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Grombcross.Models.PuzzleGameModel;

namespace Grombcross.ViewModels {
    public class PuzzleGameViewModel : ViewModelBase {
        private readonly PuzzleGameModel _puzzleGameModel;

        public Func<bool> ShowSelectView;

        public List<List<Block>> Blocks => _puzzleGameModel.Blocks;
        public List<HintLine> LeftHintLines => _puzzleGameModel.LeftHintLines;
        public List<HintLine> TopHintLines => _puzzleGameModel.TopHintLines;

        public string PuzzleName => _puzzleGameModel.CurrentPuzzle.Name;
        public Bitmap CompletedImage => _puzzleGameModel.CurrentPuzzle.FinalImage;

        public bool PuzzleSolved => _puzzleGameModel.PuzzleSolved;

        public PuzzleGameViewModel(PuzzleGameModel puzzleGameModel, Func<bool> showSelectView) {
            _puzzleGameModel = puzzleGameModel;

            ShowSelectView = showSelectView;
        }

        public void LeftClickBlock(Block block) {
            switch (block.State) {
                case Block.BlockState.EMPTY: _puzzleGameModel.FillBlock(block); break;
                case Block.BlockState.MARKED: _puzzleGameModel.FillBlock(block); break;
                case Block.BlockState.FILLED: _puzzleGameModel.ClearBlock(block); break;
            }

            block.OnPropertyChanged(nameof(block.State));

            RefreshLineFulfilledProperties(block);
            RefreshPuzzleSolved();
        }

        public void RightClickBlock(Block block) {
            switch (block.State) {
                case Block.BlockState.EMPTY: _puzzleGameModel.MarkBlock(block); break;
                case Block.BlockState.MARKED: _puzzleGameModel.ClearBlock(block); break;
                case Block.BlockState.FILLED: _puzzleGameModel.ClearBlock(block); break;
            }

            block.OnPropertyChanged(nameof(block.State));

            RefreshLineFulfilledProperties(block);
            RefreshPuzzleSolved();
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
            bool puzzleSolvedBefore = _puzzleGameModel.PuzzleSolved;
            bool puzzleSolvedNow = _puzzleGameModel.CheckForPuzzleSolved();
            if (!puzzleSolvedBefore && puzzleSolvedNow) {
                OnPropertyChanged(nameof(PuzzleSolved));
            }

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
