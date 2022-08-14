using Grombcross.Models.Systems;
using System;

namespace Grombcross.ViewModels {
    public class CreditsViewModel : ViewModelBase {
        public Func<bool> ShowSelectView;

        public CreditsViewModel(Func<bool> showSelectView) {
            ShowSelectView = () => {
                AudioSystem.PlayQuickForward();
                return showSelectView();
            };
        }
    }
}
