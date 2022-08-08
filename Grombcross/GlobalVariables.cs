using System.Collections.Generic;

namespace Grombcross {
    public static class GlobalVariables {
        public static string Version { get; } = "v0.85";

        public static List<Models.Puzzle> StandardPuzzles = new List<Models.Puzzle>();
        public static List<Models.Puzzle> BonusPuzzles = new List<Models.Puzzle>();
    }
}
