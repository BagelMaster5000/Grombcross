using System;
using System.Drawing;

namespace Grombcross.Models {
    public class Puzzle {
        public string Name { get; }
        public Bitmap GeneratorImage = null;  // new Bitmap("../Puzzle Images/TestG.bmp");
        public Bitmap FinalImage { get; }
        public int Size { get; }
        public bool Completed { get; set; }
        public DateTime TimeCompleted = DateTime.MinValue;

        public int Row { get; }
        public int Column { get; }
        public int Index { get; }

        public Puzzle(string setName, Bitmap generatorImage, Bitmap finalImage, int setSize, int setRow, int setColumn, int setIndex) {
            Name = setName;
            GeneratorImage = generatorImage;
            FinalImage = finalImage;
            Size = setSize;
            Completed = false;

            Row = setRow;
            Column = setColumn;
            Index = setIndex;
        }
    }
}
