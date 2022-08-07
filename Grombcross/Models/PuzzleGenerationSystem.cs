using Grombcross.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grombcross.Models {
    public static class PuzzleGenerationSystem {
        // Debug
        const bool PUZZLE_DEFAULT_COMPLETION = false;

        const string STANDARD_PUZZLES_DIRECTORY = "Puzzles/StandardPuzzles/";
        const string BONUS_PUZZLES_DIRECTORY = "Puzzles/BonusPuzzles/";
        const string DEBUG_PUZZLES_DIRECTORY = "Puzzles/DebugPuzzles/";

        // Puzzles located in ../Puzzles/StandardPuzzles/ and ../Puzzles/BonusPuzzles/ folders
        // Format is <#>-<Puzzle Name>-G for generation image
        // Format is <#>-<Puzzle Name>-F for final image
        public static void GeneratePuzzles() {
            GlobalVariables.StandardPuzzles = new List<Puzzle>();

            string puzzlePaths;
            string[] paths;

            // Debug Puzzles
            paths = Directory.GetFiles(Path.GetFullPath(DEBUG_PUZZLES_DIRECTORY));
            Puzzle placeholderPuzzle = GetPuzzleFromPaths(paths[1], paths[0]);


            // Standard Puzzles
            puzzlePaths = Path.GetFullPath(STANDARD_PUZZLES_DIRECTORY);
            paths = Directory.GetFiles(puzzlePaths);
            Array.Sort(paths); // Groups puzzles with same index together
            for (int p = 0; p < paths.Count(); p += 2) {
                Puzzle puzzle = GetPuzzleFromPaths(paths[p + 1], paths[p]);
                puzzle.Completed = PUZZLE_DEFAULT_COMPLETION;
                GlobalVariables.StandardPuzzles.Add(puzzle);
            }
            // Sorting
            GlobalVariables.StandardPuzzles = GlobalVariables.StandardPuzzles.OrderBy(p => p.Index).ToList(); // Sorting puzzles by index
            for (int p = 0; p < GlobalVariables.StandardPuzzles.Count; p++) { // Filling missing puzzles with placeholder
                Puzzle curPuzzle = GlobalVariables.StandardPuzzles[p];
                if (p != curPuzzle.Index) {
                    placeholderPuzzle.Index = p;
                    int numColumns = 4;
                    placeholderPuzzle.Column = p % numColumns;
                    placeholderPuzzle.Row = p / numColumns;
                    GlobalVariables.StandardPuzzles.Insert(p, placeholderPuzzle);
                    p++;
                }
            }


            // Bonus Puzzles
            puzzlePaths = Path.GetFullPath(BONUS_PUZZLES_DIRECTORY);
            paths = Directory.GetFiles(puzzlePaths);
            Array.Sort(paths); // Groups puzzles with same index together
            for (int p = 0; p < paths.Count(); p += 2) {
                Puzzle puzzle = GetPuzzleFromPaths(paths[p + 1], paths[p]);
                puzzle.Completed = PUZZLE_DEFAULT_COMPLETION;
                GlobalVariables.BonusPuzzles.Add(puzzle);
            }
            // Sorting
            GlobalVariables.BonusPuzzles = GlobalVariables.BonusPuzzles.OrderBy(p => p.Index).ToList(); // Sorting puzzles by index
            for (int p = 0; p < GlobalVariables.BonusPuzzles.Count; p++) { // Filling missing puzzles with placeholder
                Puzzle curPuzzle = GlobalVariables.BonusPuzzles[p];
                if (p != curPuzzle.Index) {
                    GlobalVariables.BonusPuzzles.Insert(p, placeholderPuzzle);
                    p++;
                }
            }
        }

        private static Puzzle GetPuzzleFromPaths(string generatorImagePath, string finalImagePath) {
            Bitmap generatorImage = new Bitmap(generatorImagePath);
            Bitmap finalImage = new Bitmap(finalImagePath);

            string gFileName = Path.GetFileNameWithoutExtension(generatorImagePath);
            string fFileName = Path.GetFileNameWithoutExtension(finalImagePath);

            string[] gWords = gFileName.Split('-');
            string[] fWords = fFileName.Split('-');

            int index;
            try {
                int gIndex = int.Parse(gWords[0]);
                int fIndex = int.Parse(fWords[0]);
                if (gIndex != fIndex) {
                    throw new InvalidPuzzlePathException("Indexes in first words do not match");
                }
                index = gIndex;
            }
            catch {
                throw new InvalidPuzzlePathException("First word error");
            }

            string name;
            try {
                string gName = gWords[1];
                string fName = fWords[1];
                if (!gName.Equals(fName)) {
                    throw new InvalidPuzzlePathException("Second words do not match");
                }
                name = gName;
            }
            catch {
                throw new InvalidPuzzlePathException("Second word error");
            }

            try {
                char gImageType = char.Parse(gWords[2]);
                char fImageType = char.Parse(fWords[2]);
                if (gImageType != 'G') {
                    throw new InvalidPuzzlePathException("Generator image doesn't have g indicator in third word. Has " + gImageType);
                }
                if (fImageType != 'F') {
                    throw new InvalidPuzzlePathException("Final image doesn't have f indicator in third word. Has " + gImageType);
                }
            }
            catch {
                throw new InvalidPuzzlePathException("Third word error");
            }

            int numColumns = 4;
            int column = index % numColumns;
            int row = index / numColumns;

            Puzzle puzzle = new Puzzle(name, generatorImage, finalImage, generatorImage.Size.Width, row, column, index);
            return puzzle;
        }
    }
}
