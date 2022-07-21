using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grombcross.Models
{
    public class PuzzleGameModel
    {
        public Puzzle CurrentPuzzle;
        public int CurrentPuzzleIndex;

        public List<List<Block>> Blocks;
        public int BlockGridSize;
        public List<string> TopNumberStrings;
        public List<string> LeftNumberStrings;

        public bool PuzzleSolved = false;
        public List<List<bool>> PuzzleSolution;
        public List<bool> RowsSolved;
        public List<bool> ColumnsSolved;

        public DateTime ElapsedTime;

        public PuzzleGameModel(int setPuzzleIndex)
        {
            CurrentPuzzleIndex = setPuzzleIndex;
            CurrentPuzzle = GlobalVariables.Puzzles[CurrentPuzzleIndex];

            // Generate Blocks and Strings from Image
        }

        private void GenerateBlocksAndStringsFromImage()
        {

        }
    }
}
