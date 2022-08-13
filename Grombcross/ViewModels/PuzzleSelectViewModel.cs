using Grombcross.Models;
using Grombcross.Models.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Grombcross.ViewModels {
    public class PuzzleSelectViewModel : ViewModelBase {
        public List<Puzzle> AllPuzzles => GlobalVariables.StandardPuzzles;

        public Func<bool> ShowTitleView;
        public Func<bool> ShowCreditsView;
        public Func<int, bool> ShowGameView;

        public PuzzleSelectViewModel(Func<bool> showTitleView, Func<bool> showCreditsView, Func<int, bool> showGameView) {
            AudioSystem.StartMusic();

            ShowTitleView = () => {
                AudioSystem.StopMusic();
                AudioSystem.PlayQuickReturn();
                return showTitleView();
            };

            ShowCreditsView = () => {
                AudioSystem.PlayQuickReturn();
                return showCreditsView();
            };

            ShowGameView = (puzzleIndex) => {
                AudioSystem.PlayQuickForward();
                AudioSystem.StopMusic();
                AudioSystem.PlayPuzzleStart();
                return showGameView(puzzleIndex);
            };
        }
    }
}
