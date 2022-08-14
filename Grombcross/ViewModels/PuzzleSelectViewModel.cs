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
        public List<Puzzle> AllPuzzles {
            get {
                switch (GlobalVariables.PuzzleSource) {
                    case GlobalVariables.PuzzleSourceType.STANDARD:
                    default:
                        return GlobalVariables.StandardPuzzles;
                    case GlobalVariables.PuzzleSourceType.BONUS:
                        return GlobalVariables.BonusPuzzles;
                }
            }
        }

        public Func<bool> ShowTitleView;
        public Func<bool> ShowCreditsView;
        public Func<int, bool> ShowGameView;
        public Func<bool> ShowPuzzleSelectView;
        public Func<bool> ShowSettingsView;

        public string HeaderText {
            get {
                switch (GlobalVariables.PuzzleSource) {
                    case GlobalVariables.PuzzleSourceType.STANDARD: default: return "Puzzle Select";
                    case GlobalVariables.PuzzleSourceType.BONUS: return "Bonus Puzzle Select";
                }
            }
        }

        public string ShowPuzzleSelectButtonText {
            get {
                switch (GlobalVariables.PuzzleSource) {
                    case GlobalVariables.PuzzleSourceType.STANDARD: default: return "Bonus Puzzles";
                    case GlobalVariables.PuzzleSourceType.BONUS: return "Standard Puzzles";
                }
            }
        }

        public PuzzleSelectViewModel(Func<bool> showTitleView, Func<bool> showCreditsView, Func<int, bool> showGameView,
            Func<bool> showStandardPuzzlesView, Func<bool> showBonusPuzzlesView, Func<bool> showSettingsView) {
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

            ShowPuzzleSelectView = () => {
                AudioSystem.PlayQuickForward();

                switch (GlobalVariables.PuzzleSource) {
                    case GlobalVariables.PuzzleSourceType.STANDARD:
                    default:
                        return showBonusPuzzlesView();
                    case GlobalVariables.PuzzleSourceType.BONUS:
                        return showStandardPuzzlesView();
                }
            };

            ShowSettingsView = () => {
                AudioSystem.PlayQuickReturn();
                return showSettingsView();
            };

            AudioSystem.StartMusic();
        }
    }
}
