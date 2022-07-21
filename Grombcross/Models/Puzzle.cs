using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grombcross.Models
{
    public class Puzzle
    {
        public Bitmap GeneratorImage = null;  // new Bitmap("../Puzzle Images/TestG.bmp");
        public Bitmap FinalImage = null;
        public bool Completed = false;
        public DateTime TimeCompleted = DateTime.MinValue;

        public Puzzle(Bitmap generatorImage, Bitmap finalImage)
        {
            GeneratorImage = generatorImage;
            FinalImage = finalImage;
        }
    }
}
