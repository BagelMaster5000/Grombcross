using System;
using System.Drawing;

namespace Grombcross.Models {
    public class Puzzle {
        public string Name;
        public Bitmap GeneratorImage = null;  // new Bitmap("../Puzzle Images/TestG.bmp");
        public Bitmap FinalImage { get; }
        public bool Completed = false;
        public DateTime TimeCompleted = DateTime.MinValue;

        public int Row { get; }
        public int Column { get; }
        public int Index { get; }

        public Puzzle(string setName, Bitmap generatorImage, Bitmap finalImage, int setRow, int setColumn, int setIndex) {
            Name = setName;
            GeneratorImage = generatorImage;
            FinalImage = finalImage;

            Row = setRow;
            Column = setColumn;
            Index = setIndex;
        }
    }
}
