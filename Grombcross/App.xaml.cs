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

namespace Grombcross {

    public partial class App : Application {
        public App() {
        }

        protected override void OnStartup(StartupEventArgs e) {
            GeneratePuzzles();

            MainWindow = new MainWindow() {
                DataContext = new MainViewModel()
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        public void GeneratePuzzles() {
            GlobalVariables.Puzzles = new List<Puzzle>();

            string fullPath = Path.GetFullPath(@"PuzzleImages\TestSmall.bmp");
            Bitmap test = new Bitmap(fullPath);
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 0, 0));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 0, 1));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 0, 2));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 0, 3));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 1, 0));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 1, 1));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 1, 2));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 1, 3));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 2, 0));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 2, 1));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 2, 2));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 2, 3));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 3, 0));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 3, 1));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 3, 2));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 3, 3));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 4, 0));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 4, 1));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 4, 2));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 4, 3));
            GlobalVariables.Puzzles.Add(new Puzzle("Creature", test, test, 5, 0));
        }
    }
}
