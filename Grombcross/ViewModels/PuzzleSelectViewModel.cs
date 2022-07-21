using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grombcross.ViewModels
{
    public class PuzzleSelectViewModel : ViewModelBase
    {
        public Func<bool> ShowCreditsView;
        public Func<int, bool> ShowGameView;

        public PuzzleSelectViewModel(Func<bool> showCreditsView, Func<int, bool> showGameView)
        {
            ShowCreditsView = showCreditsView;
            ShowGameView = showGameView;
        }
    }
}
