﻿using Grombcross.Exceptions;
using Grombcross.Models.Systems;
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
        public List<HintLine> LeftHintLines;
        public List<HintLine> TopHintLines;

        public bool PuzzleSolved = false;
        public List<List<bool>> PuzzleSolution;

        public DateTime ElapsedTime;

        #region Setup
        public PuzzleGameModel(int setPuzzleIndex) {
            try {
                switch (GlobalVariables.PuzzleSource) {
                    case GlobalVariables.PuzzleSourceType.STANDARD:
                        CurrentPuzzle = GlobalVariables.StandardPuzzles[setPuzzleIndex];
                        break;
                    case GlobalVariables.PuzzleSourceType.BONUS:
                        CurrentPuzzle = GlobalVariables.BonusPuzzles[setPuzzleIndex];
                        break;
                }
                if (CurrentPuzzle.Index != setPuzzleIndex) {
                    throw new PuzzleNotFoundException(GlobalVariables.PuzzleSource + " puzzles: Index mismatch of " + setPuzzleIndex + " and " + CurrentPuzzle.Index);
                }
            }
            catch {
                throw new PuzzleNotFoundException(GlobalVariables.PuzzleSource + " puzzles: Could not find puzzle at index " + CurrentPuzzleIndex);
            }

            CurrentPuzzleIndex = setPuzzleIndex;

            GeneratePuzzle();
            AutoXBlankLines();
        }

        #region Generate Puzzle
        public void GeneratePuzzle() {
            if (CurrentPuzzle.GeneratorImage.Width !=
                CurrentPuzzle.GeneratorImage.Height) {
                throw new WidthHeightUnequalException();
            }

            Bitmap gImage = CurrentPuzzle.GeneratorImage;
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
                    Blocks[x].Add(new Block(x, y));
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

                List<int> hintNumbers = new List<int>();

                List<bool> curRowSolution = PuzzleSolution[r];
                string curRowString = "";
                int counter = 0;
                for (int c = 0; c < BlockGridSize; c++) {
                    if (curRowSolution[c]) {
                        counter++;
                    }
                    else if (counter > 0) {
                        curRowString += counter.ToString() + " ";
                        hintNumbers.Add(counter);
                        counter = 0;
                    }
                }
                if (counter > 0) {
                    curRowString += counter.ToString();
                    hintNumbers.Add(counter);
                }
                else if (string.IsNullOrEmpty(curRowString)) {
                    curRowString = "0";
                    hintNumbers.Add(0);
                }
                curRowString = curRowString.TrimEnd(' ');

                Trace.WriteLine(curRowString);
                LeftHintLines.Add(new HintLine { HintNumbersString = curRowString, LineFulfilled = false, HintNumbers = hintNumbers });
            }
        }

        private void GenerateTopNumberLines() {
            Trace.WriteLine("");
            Trace.WriteLine("Columns:");

            TopHintLines = new List<HintLine>();
            for (int c = 0; c < BlockGridSize; c++) {

                List<int> hintNumbers = new List<int>();

                string curColString = "";
                int counter = 0;
                for (int r = 0; r < BlockGridSize; r++) {
                    if (PuzzleSolution[r][c]) {
                        counter++;
                    }
                    else if (counter > 0) {
                        curColString += counter.ToString() + '\n';
                        hintNumbers.Add(counter);
                        counter = 0;
                    }
                }
                if (counter > 0) {
                    curColString += counter.ToString();
                    hintNumbers.Add(counter);
                }
                else if (string.IsNullOrEmpty(curColString)) {
                    curColString = "0";
                    hintNumbers.Add(0);
                }
                curColString = curColString.TrimEnd('\n');

                Trace.WriteLine(curColString);
                TopHintLines.Add(new HintLine { HintNumbersString = curColString, LineFulfilled = false, HintNumbers = hintNumbers });
            }
        }
        #endregion
        #endregion

        public void ResetPuzzle() {
            ClearPuzzle();
            AutoXBlankLines();

            CheckForPuzzleSolved();
        }

        private void ClearPuzzle() {
            for (int r = 0; r < BlockGridSize; r++) {
                for (int c = 0; c < BlockGridSize; c++) {
                    Blocks[r][c].ClearBlock();
                }
            }
        }

        public void AutoXBlankLines() {
            for (int r = 0; r < BlockGridSize; r++) {
                if (LeftHintLines[r].HintNumbers.Count == 1 && LeftHintLines[r].HintNumbers[0] == 0) {
                    XRow(r);
                }
            }

            for (int c = 0; c < BlockGridSize; c++) {
                if (TopHintLines[c].HintNumbers.Count == 1 && TopHintLines[c].HintNumbers[0] == 0) {
                    XColumn(c);
                }
            }
        }

        public bool CheckForPuzzleSolved() {
            bool puzzleSolved = true;

            for (int r = 0; r < BlockGridSize; r++) {
                bool curRowFulfilled = CheckRowFulfilled(r);
                if (!curRowFulfilled) {
                    puzzleSolved = false;
                }
            }

            for (int c = 0; c < BlockGridSize; c++) {
                bool curColumnFulfilled = CheckColumnFulfilled(c);
                if (!curColumnFulfilled) {
                    puzzleSolved = false;
                }
            }

            PuzzleSolved = puzzleSolved;
            if (PuzzleSolved) {
                PuzzleComplete();
            }
            return PuzzleSolved;
        }
        public bool CheckRowFulfilled(int r) {
            bool rowFulfilled = true;

            int counter = 0;
            int curHintNumber = 0;
            for (int c = 0; c < BlockGridSize; c++) {

                if (Blocks[r][c].State == Block.BlockState.FILLED) {
                    counter++;
                }
                else if (counter > 0) {
                    if (curHintNumber >= LeftHintLines[r].HintNumbers.Count || LeftHintLines[r].HintNumbers[curHintNumber] != counter) {
                        rowFulfilled = false;
                    }
                    counter = 0;
                    curHintNumber++;
                }
            }
            if (counter > 0) {
                if (curHintNumber >= LeftHintLines[r].HintNumbers.Count || LeftHintLines[r].HintNumbers[curHintNumber] != counter) {
                    rowFulfilled = false;
                }
                counter = 0;
                curHintNumber++;
            }
            if (curHintNumber != LeftHintLines[r].HintNumbers.Count && LeftHintLines[r].HintNumbers[0] != 0)
                rowFulfilled = false;

            LeftHintLines[r].LineFulfilled = rowFulfilled;
            return rowFulfilled;
        }
        public bool CheckColumnFulfilled(int c) {

            bool columnFulfilled = true;

            int counter = 0;
            int curHintNumber = 0;
            for (int r = 0; r < BlockGridSize; r++) {

                if (Blocks[r][c].State == Block.BlockState.FILLED) {
                    counter++;
                }
                else if (counter > 0) {
                    if (curHintNumber >= TopHintLines[c].HintNumbers.Count || TopHintLines[c].HintNumbers[curHintNumber] != counter) {
                        columnFulfilled = false;
                    }
                    counter = 0;
                    curHintNumber++;
                }
            }
            if (counter > 0) {
                if (curHintNumber >= TopHintLines[c].HintNumbers.Count || TopHintLines[c].HintNumbers[curHintNumber] != counter) {
                    columnFulfilled = false;
                }
                counter = 0;
                curHintNumber++;
            }
            if (curHintNumber != TopHintLines[c].HintNumbers.Count && TopHintLines[c].HintNumbers[0] != 0)
                columnFulfilled = false;

            TopHintLines[c].LineFulfilled = columnFulfilled;
            return columnFulfilled;
        }

        private void PuzzleComplete() {
            CurrentPuzzle.Completed = true;

            SaveSystem.SaveGame();
        }

        public void FillBlock(Block block) {
            block.FillBlock();
        }
        public void XBlock(Block block) {
            block.XBlock();
        }
        public void QuestionBlock(Block block) {
            block.QuestionBlock();
        }
        public void ClearBlock(Block block) {
            block.ClearBlock();
        }

        internal bool QuickFillIntersectLines(Block block) {
            bool fillSuccessful = false;

            HintLine rowLine = LeftHintLines[block.Row];
            if (rowLine.HintNumbers[0] == BlockGridSize) {
                FillRow(block.Row);
                fillSuccessful = true;
            }

            HintLine columnLine = TopHintLines[block.Column];
            if (columnLine.HintNumbers[0] == BlockGridSize) {
                FillColumn(block.Column);
                fillSuccessful = true;
            }

            return fillSuccessful;
        }
        internal bool QuickXIntersectLines(Block block) {
            bool xSuccessful = false;

            HintLine rowLine = LeftHintLines[block.Row];
            if (rowLine.LineFulfilled) {
                XRowIgnoringFilledBlocks(block.Row);
                xSuccessful = true;
            }

            HintLine columnLine = TopHintLines[block.Column];
            if (columnLine.LineFulfilled) {
                XColumnIgnoringFilledBlocks(block.Column);
                xSuccessful = true;
            }

            return xSuccessful;
        }

        private void FillRow(int r) {
            for (int c = 0; c < BlockGridSize; c++) {
                Blocks[r][c].FillBlock();
            }
        }
        private void FillColumn(int c) {
            for (int r = 0; r < BlockGridSize; r++) {
                Blocks[r][c].FillBlock();
            }
        }
        private void XRow(int r) {
            for (int c = 0; c < BlockGridSize; c++) {
                Blocks[r][c].XBlock();
            }
        }
        private void XColumn(int c) {
            for (int r = 0; r < BlockGridSize; r++) {
                Blocks[r][c].XBlock();
            }
        }
        private void XRowIgnoringFilledBlocks(int r) {
            for (int c = 0; c < BlockGridSize; c++) {
                if (Blocks[r][c].State == Block.BlockState.EMPTY) {
                    Blocks[r][c].XBlock();
                }
            }
        }
        private void XColumnIgnoringFilledBlocks(int c) {
            for (int r = 0; r < BlockGridSize; r++) {
                if (Blocks[r][c].State == Block.BlockState.EMPTY) {
                    Blocks[r][c].XBlock();
                }
            }
        }
    }
}
