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

        public PuzzleSelectViewModel(Func<bool> showCreditsView, Func<int, bool> showGameView) {
            AudioManager.StartMusic();

            ShowCreditsView = () => {
                AudioManager.PlayQuickReturn();
                return showCreditsView();
            };

            ShowGameView = (puzzleIndex) => {
                AudioManager.PlayQuickForward();
                AudioManager.StopMusic();
                AudioManager.PlayPuzzleStart();
                return showGameView(puzzleIndex);
            };
        }
    }
}
