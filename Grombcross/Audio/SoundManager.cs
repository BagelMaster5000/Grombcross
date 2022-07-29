using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Grombcross.Audio {
    public static class SoundManager {
        public static SoundPlayer BlockPlace = new SoundPlayer(Properties.Resources.PlacedBlock);
        public static void PlaceBlockPlay() => BlockPlace.Play();

        public static SoundPlayer XPlace = new SoundPlayer(Properties.Resources.PlacedX);
        public static void XBlockPlay() => XPlace.Play();

        public static SoundPlayer PuzzleComplete = new SoundPlayer(Properties.Resources.Complete);
        public static void PuzzleCompletePlay() => PuzzleComplete.Play();

        public static SoundPlayer PuzzleStart = new SoundPlayer(Properties.Resources.Start);
        public static void PuzzleStartPlay() => PuzzleStart.Play();

        public static SoundPlayer Return = new SoundPlayer(Properties.Resources.Return);
        public static void BackPlay() => Return.Play();

        public static void StartMusic() {

        }

        public static void StopMusic() {

        }
    }
}
