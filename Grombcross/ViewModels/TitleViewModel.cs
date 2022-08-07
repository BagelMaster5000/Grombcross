using Grombcross.Models;
using System;

namespace Grombcross.ViewModels {
    public class TitleViewModel : ViewModelBase {
        public Func<bool> ShowSelectView;

        public TitleViewModel(Func<bool> showSelectView) {
            ShowSelectView = () => {
                AudioSystem.PlayPuzzleStart();
                return showSelectView();
            };
        }
    }
}
