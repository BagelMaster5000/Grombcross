using Grombcross.Models.Systems;
using System;

namespace Grombcross.ViewModels {
    public class TitleViewModel : ViewModelBase {
        public Func<bool> ShowSelectView;

        public string Version => GlobalVariables.Version;

        public TitleViewModel(Func<bool> showSelectView) {
            //AudioSystem.StartMusic();

            ShowSelectView = () => {
                AudioSystem.PlayPuzzleStart();
                return showSelectView();
            };
        }
    }
}
