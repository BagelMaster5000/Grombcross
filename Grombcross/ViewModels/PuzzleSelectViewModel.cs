using Grombcross.Audio;
using Grombcross.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Grombcross.ViewModels {
    public class PuzzleSelectViewModel : ViewModelBase {
        public List<Puzzle> AllPuzzles => GlobalVariables.StandardPuzzles;

        public Func<bool> ShowCreditsView;
        public Func<int, bool> ShowGameView;

        private SoundPlayer _puzzleStart;

        public PuzzleSelectViewModel(Func<bool> showCreditsView, Func<int, bool> showGameView) {
            _puzzleStart = new SoundPlayer(Properties.Resources.Start);

            ShowCreditsView = showCreditsView;
            ShowGameView = (puzzleIndex) => {
                SoundManager.PuzzleStart.Play();
                return showGameView(puzzleIndex);
            };
        }
    }
}
