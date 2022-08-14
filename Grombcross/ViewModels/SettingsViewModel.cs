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
        public void ToggleMusicMute() => AudioSystem.ToggleMusicMute();

        public bool SfxMuted { get { return AudioSystem.SfxMuted; } }
        public void ToggleSfxMute() => AudioSystem.ToggleSfxMute();

        public void ResetStandardPuzzlesSaveData() => SaveSystem.ResetStandardPuzzlesSaveData();
        public void ResetBonusPuzzlesSaveData() => SaveSystem.ResetBonusPuzzlesSaveData();
    }
}
