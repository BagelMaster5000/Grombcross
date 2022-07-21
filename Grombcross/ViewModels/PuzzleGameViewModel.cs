using Grombcross.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grombcross.ViewModels
{
    public class PuzzleGameViewModel : ViewModelBase
    {
        private PuzzleGameModel _puzzleGameModel;

        public Func<bool> ShowSelectView;

        public PuzzleGameViewModel(PuzzleGameModel puzzleGameModel, Func<bool> showSelectView)
        {
            _puzzleGameModel = puzzleGameModel;

            ShowSelectView = showSelectView;
        }
    }
}
