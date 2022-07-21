using Grombcross.Models;
using Grombcross.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Grombcross
{

    public partial class App : Application
    {
        public App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            GeneratePuzzles();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel()
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        public void GeneratePuzzles()
        {
            GlobalVariables.Puzzles = new List<Puzzle>();

            string fullPath = Path.GetFullPath(@"PuzzleImages\TestG.bmp");
            Bitmap test = new Bitmap(@"PuzzleImages\TestG.bmp");
            Puzzle puzzle = new Puzzle(test, test);
            GlobalVariables.Puzzles.Add(puzzle);
        }
    }
}
