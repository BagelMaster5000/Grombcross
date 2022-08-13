using Grombcross.Models.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
