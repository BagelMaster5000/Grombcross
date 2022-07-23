using Grombcross.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;

namespace Grombcross.Models {
    public class PuzzleGameModel {
        public int CurrentPuzzleIndex;
        public Puzzle CurrentPuzzle;

        public List<List<Block>> Blocks;
        public int BlockGridSize;
        public struct HintLine {
            public string LineString { get; set; }
            public bool LineSolved { get; set; }
        }
        public List<HintLine> LeftHintLines;
        public List<HintLine> TopHintLines;

        public bool PuzzleSolved = false;
        public List<List<bool>> PuzzleSolution;

        public DateTime ElapsedTime;

        public PuzzleGameModel(int setPuzzleIndex) {
            CurrentPuzzleIndex = setPuzzleIndex;
            CurrentPuzzle = GlobalVariables.Puzzles[CurrentPuzzleIndex];

            PuzzleGeneration();
        }

        private void PuzzleGeneration() {
            if (GlobalVariables.Puzzles[CurrentPuzzleIndex].GeneratorImage.Width !=
                GlobalVariables.Puzzles[CurrentPuzzleIndex].GeneratorImage.Height) {
                throw new WidthHeightUnequalException();
            }

            Bitmap gImage = GlobalVariables.Puzzles[CurrentPuzzleIndex].GeneratorImage;
            GenerateBlankBlockGrid(gImage);
            GenerateSolutionGrid(gImage);
            GenerateLeftNumberLines();
            GenerateTopNumberLines();

            CheckForPuzzleSolved();
        }

        private void GenerateBlankBlockGrid(Bitmap gImage) {
            BlockGridSize = gImage.Width;
            Blocks = new List<List<Block>>();
            for (int x = 0; x < BlockGridSize; x++) {
                Blocks.Add(new List<Block>());
                for (int y = 0; y < BlockGridSize; y++) {
                    Blocks[x].Add(new Block());
                }
            }
        }

        private void GenerateSolutionGrid(Bitmap gImage) {
            Color fillColor = Color.FromArgb(255, 0, 0, 0);
            PuzzleSolution = new List<List<bool>>();
            for (int x = 0; x < BlockGridSize; x++) {
                PuzzleSolution.Add(new List<bool>());
                for (int y = 0; y < BlockGridSize; y++) {
                    Color curPixelColor = gImage.GetPixel(y, x);
                    bool curBlockFilled = curPixelColor.Equals(fillColor);
                    PuzzleSolution[x].Add(curBlockFilled);
                }
            }
        }

        private void GenerateLeftNumberLines() {
            Trace.WriteLine("");
            Trace.WriteLine("Rows:");

            LeftHintLines = new List<HintLine>();
            for (int r = 0; r < BlockGridSize; r++) {

                List<bool> curRowSolution = PuzzleSolution[r];
                string curRowString = "";
                int counter = 0;
                for (int c = 0; c < BlockGridSize; c++) {
                    if (curRowSolution[c]) {
                        counter++;
                    }
                    else if (counter > 0) {
                        curRowString += counter.ToString() + " ";
                        counter = 0;
                    }
                }
                if (counter > 0) {
                    curRowString += counter.ToString();
                }
                else if (string.IsNullOrEmpty(curRowString)) {
                    curRowString = "0";
                }
                curRowString = curRowString.TrimEnd(' ');

                Trace.WriteLine(curRowString);
                LeftHintLines.Add(new HintLine { LineString = curRowString, LineSolved = false });
            }
        }

        private void GenerateTopNumberLines() {
            Trace.WriteLine("");
            Trace.WriteLine("Columns:");

            TopHintLines = new List<HintLine>();
            for (int c = 0; c < BlockGridSize; c++) {

                string curColString = "";
                int counter = 0;
                for (int r = 0; r < BlockGridSize; r++) {
                    if (PuzzleSolution[r][c]) {
                        counter++;
                    }
                    else if (counter > 0) {
                        curColString += counter.ToString() + '\n';
                        counter = 0;
                    }
                }
                if (counter > 0) {
                    curColString += counter.ToString();
                }
                else if (string.IsNullOrEmpty(curColString)) {
                    curColString = "0";
                }
                curColString = curColString.TrimEnd('\n');

                Trace.WriteLine(curColString);
                TopHintLines.Add(new HintLine { LineString = curColString, LineSolved = false });
            }
        }

        private void CheckForPuzzleSolved() {
            bool puzzleSolved = true;

            for (int i = 0; i < BlockGridSize; i++) {
                bool curRowSolved = GetIsRowSolved(i);
                HintLine leftHintLine = LeftHintLines[i];
                leftHintLine.LineSolved = curRowSolved;

                bool curColumnSolved = GetIsColumnSolved(i);
                HintLine topHintLine = TopHintLines[i];
                topHintLine.LineSolved = curColumnSolved;

                if (!curRowSolved || !curColumnSolved)
                    puzzleSolved = false;
            }

            PuzzleSolved = puzzleSolved;
        }

        private bool GetIsRowSolved(int r) {
            for (int c = 0; c < BlockGridSize; c++) {
                bool blockCorrect = (Blocks[r][c].State == Block.BlockState.FILLED && PuzzleSolution[r][c]) ||
                    (Blocks[r][c].State != Block.BlockState.FILLED && !PuzzleSolution[r][c]);

                if (!blockCorrect) { return false; }
            }

            return true;
        }

        private bool GetIsColumnSolved(int c) {
            for (int r = 0; r < BlockGridSize; r++) {
                bool blockCorrect = (Blocks[r][c].State == Block.BlockState.FILLED && PuzzleSolution[r][c]) ||
                    (Blocks[r][c].State != Block.BlockState.FILLED && !PuzzleSolution[r][c]);

                if (!blockCorrect) { return false; }
            }

            return true;
        }

        public void FillBlock(Block block) {
            block.State = Block.BlockState.FILLED;
        }
        public void MarkBlock(Block block) {
            block.State = Block.BlockState.MARKED;
        }
        public void ClearBlock(Block block) {
            block.State = Block.BlockState.EMPTY;
        }
    }
}
