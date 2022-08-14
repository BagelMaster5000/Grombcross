using System.Collections.Generic;

namespace Grombcross {
    public static class GlobalVariables {
        public const string VERSION = "v0.85";

        public static List<Models.Puzzle> StandardPuzzles = new List<Models.Puzzle>();
        public static List<Models.Puzzle> BonusPuzzles = new List<Models.Puzzle>();

        public enum PuzzleSourceType { STANDARD, BONUS };
        public static PuzzleSourceType PuzzleSource = PuzzleSourceType.STANDARD;

        public static int NumPuzzleSelectColumns = 5;
    }
}
