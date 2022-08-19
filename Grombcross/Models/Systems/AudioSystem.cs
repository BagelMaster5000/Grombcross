using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Grombcross.Models.Systems {
    public static class AudioSystem {
        public static void InitializeMediaPlayers() {
            string path = Path.GetFullPath(@"Audio\SFX\PlacedBlock.wav");
            _blockPlace.Open(new Uri(path));
            _blockPlace.Volume = 1;
            _blockPlace.Position = TimeSpan.MaxValue;
            path = Path.GetFullPath(@"Audio\SFX\PlacedX.wav");
            _xPlace.Open(new Uri(path));
            _xPlace.Volume = 1;
            _xPlace.Position = TimeSpan.MaxValue;

            path = Path.GetFullPath(@"Audio\SFX\Complete.wav");
            _puzzleComplete.Open(new Uri(path));
            _puzzleComplete.Volume = 1;
            _puzzleComplete.Position = TimeSpan.MaxValue;
            path = Path.GetFullPath(@"Audio\SFX\Start.wav");
            _puzzleStart.Open(new Uri(path));
            _puzzleStart.Volume = 1;
            _puzzleStart.Position = TimeSpan.MaxValue;
            path = Path.GetFullPath(@"Audio\SFX\Return.wav");
            _longReturn.Open(new Uri(path));
            _longReturn.Volume = 1;
            _longReturn.Position = TimeSpan.MaxValue;

            path = Path.GetFullPath(@"Audio\Music\Music.wav");
            _music.Open(new Uri(path));
            _music.Volume = 0.5;
            _music.MediaEnded += (object? sender, EventArgs e) => {
                _music.Position = TimeSpan.Zero;
                _music.Play();
            };
        }


        #region SFX
        private static MediaPlayer _blockPlace = new MediaPlayer();
        public static void PlayBlockPlace() {
            _blockPlace.Position = TimeSpan.Zero;
            _blockPlace.Play();
        }

        private static MediaPlayer _xPlace = new MediaPlayer();
        public static void PlayXPlace() {
            _xPlace.Position = TimeSpan.Zero;
            _xPlace.Play();
        }

        private static MediaPlayer _puzzleComplete = new MediaPlayer();
        public static void PlayPuzzleComplete() {
            _puzzleComplete.Position = TimeSpan.Zero;
            _puzzleComplete.Play();
        }

        private static MediaPlayer _puzzleStart = new MediaPlayer();
        public static void PlayPuzzleStart() {
            _puzzleStart.Position = TimeSpan.Zero;
            _puzzleStart.Play();
        }

        private static MediaPlayer _longReturn = new MediaPlayer();
        public static void PlayLongReturn() {
            _longReturn.Position = TimeSpan.Zero;
            _longReturn.Play();
        }
        public static void PlayQuickReturn() {
            _xPlace.Volume = 1;
            _xPlace.Position = TimeSpan.Zero;
            _xPlace.Play();
        }
        public static void PlayQuickForward() {
            _blockPlace.Volume = 0.5f;
            _blockPlace.Position = TimeSpan.Zero;
            _blockPlace.Play();
        }
        #endregion


        #region Music
        private static MediaPlayer _music = new MediaPlayer();
        public static void StartMusic(object? sender = null, EventArgs? e = null) {
            _music.Play();
        }
        public static void StopMusic() {
            _music.Stop();
        }
        #endregion


        #region Muting
        private static bool _sfxMuted;
        public static bool SfxMuted { get { return _sfxMuted; } set { _sfxMuted = value; RefreshAllSfxMute(); } }
        public static void ToggleSfxMute() {
            SfxMuted = !SfxMuted;
        }
        private static void RefreshAllSfxMute() {
            _blockPlace.IsMuted = _sfxMuted;
            _xPlace.IsMuted = _sfxMuted;
            _puzzleComplete.IsMuted = _sfxMuted;
            _puzzleStart.IsMuted = _sfxMuted;
            _longReturn.IsMuted = _sfxMuted;
        }

        public static bool MusicMuted { get { return _music.IsMuted; } set { _music.IsMuted = value; } }
        public static void ToggleMusicMute() {
            MusicMuted = !MusicMuted;
        }
        #endregion
    }
}
