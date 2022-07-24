using Grombcross.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grombcross.ViewModels {
    public class MainViewModel : ViewModelBase {
        public ViewModelBase CurrentViewModel { get; set; }

        public MainViewModel() {
            ShowPuzzleSelectView();
        }

        public bool ShowPuzzleSelectView() {
            CurrentViewModel = new PuzzleSelectViewModel(ShowCreditsView, ShowPuzzleGameView);
            OnPropertyChanged(nameof(CurrentViewModel));
            return true;
        }

        public bool ShowPuzzleGameView(int puzzleIndex) {
            PuzzleGameModel gameModel = new PuzzleGameModel(puzzleIndex);
            CurrentViewModel = new PuzzleGameViewModel(gameModel, ShowPuzzleSelectView);
            OnPropertyChanged(nameof(CurrentViewModel));
            return true;
        }

        public bool ShowCreditsView() {
            CurrentViewModel = new CreditsViewModel(ShowPuzzleSelectView);
            OnPropertyChanged(nameof(CurrentViewModel));
            return true;
        }
    }
}
