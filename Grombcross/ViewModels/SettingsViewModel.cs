using Grombcross.Models.Systems;
using System;

namespace Grombcross.ViewModels {
    public class SettingsViewModel : ViewModelBase {
        public Func<bool> ShowSelectView;

        public SettingsViewModel(Func<bool> showSelectView) {
            ShowSelectView = () => {
                AudioSystem.PlayQuickForward();
                return showSelectView();
            };
        }

        public bool MusicMuted { get { return AudioSystem.MusicMuted; } }
        public void ToggleMusicMute() {
            AudioSystem.ToggleMusicMute();

            AudioSystem.PlayQuickReturn();
        }

        public bool SfxMuted { get { return AudioSystem.SfxMuted; } }
        public void ToggleSfxMute() {
            AudioSystem.ToggleSfxMute();

            AudioSystem.PlayQuickReturn();
        }

        public void ResetStandardPuzzlesSaveData() {
            SaveSystem.ResetStandardPuzzlesSaveData();

            AudioSystem.PlayLongReturn();
        }
        public void ResetBonusPuzzlesSaveData() {
            SaveSystem.ResetBonusPuzzlesSaveData();

            AudioSystem.PlayLongReturn();
        }
    }
}
