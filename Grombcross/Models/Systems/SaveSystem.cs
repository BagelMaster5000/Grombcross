using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Grombcross.Models.Systems {
    public static class SaveSystem {
        const string SAVE_DIRECTORY = "SaveData/";

        #region Saving Game
        public static void SaveGame() {
            SaveStandardPuzzles();
            SaveBonusPuzzles();

            SaveSettings();
        }
        private static void SaveStandardPuzzles() {
            Dictionary<string, bool> standardPuzzlesSaveData = new Dictionary<string, bool>();
            foreach (Puzzle p in GlobalVariables.StandardPuzzles) {
                if (!standardPuzzlesSaveData.ContainsKey(p.Name)) {
                    standardPuzzlesSaveData.Add(p.Name, p.Completed);
                }
            }

            SaveDictionaryToJson(standardPuzzlesSaveData, "StandardPuzzlesSaveData");
        }
        private static void SaveBonusPuzzles() {
            Dictionary<string, bool> bonusPuzzlesSaveData = new Dictionary<string, bool>();
            foreach (Puzzle p in GlobalVariables.BonusPuzzles) {
                if (!bonusPuzzlesSaveData.ContainsKey(p.Name)) {
                    bonusPuzzlesSaveData.Add(p.Name, p.Completed);
                }
            }

            SaveDictionaryToJson(bonusPuzzlesSaveData, "BonusPuzzlesSaveData");
        }
        private static void SaveSettings() {
            Dictionary<string, bool> settingsSaveData = new Dictionary<string, bool>();
            settingsSaveData.Add("MusicMuted", AudioSystem.MusicMuted);
            settingsSaveData.Add("SfxMuted", AudioSystem.SfxMuted);

            SaveDictionaryToJson(settingsSaveData, "SettingsSaveData");
        }

        private static void SaveDictionaryToJson(Dictionary<string, bool> puzzlesSaveData, string saveFileName) {
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions() { WriteIndented = true };
            string json = JsonSerializer.Serialize(puzzlesSaveData, jsonOptions); ;

            Directory.CreateDirectory(SAVE_DIRECTORY);
            File.WriteAllText(SAVE_DIRECTORY + saveFileName + ".json", json);
        }
        #endregion


        #region Loading Game
        public static void LoadGame() {
            LoadStandardPuzzles();
            LoadBonusPuzzles();

            LoadSettings();
        }
        private static void LoadStandardPuzzles() {
            Dictionary<string, bool>? standardPuzzlesSaveData = LoadPuzzlesFromJson("StandardPuzzlesSaveData");
            if (standardPuzzlesSaveData != null) {
                foreach (Puzzle p in GlobalVariables.StandardPuzzles) {
                    p.Completed = standardPuzzlesSaveData.GetValueOrDefault(p.Name);
                }
            }
        }
        private static void LoadBonusPuzzles() {
            Dictionary<string, bool>? bonusPuzzlesSaveData = LoadPuzzlesFromJson("BonusPuzzlesSaveData");
            if (bonusPuzzlesSaveData == null) {
                return;
            }

            foreach (Puzzle p in GlobalVariables.BonusPuzzles) {
                p.Completed = bonusPuzzlesSaveData.GetValueOrDefault(p.Name);
            }
        }
        private static void LoadSettings() {
            Dictionary<string, bool>? settingsSaveData = LoadPuzzlesFromJson("SettingsSaveData");
            if (settingsSaveData == null) {
                return;
            }

            AudioSystem.SfxMuted = settingsSaveData.GetValueOrDefault("SfxMuted");
            AudioSystem.MusicMuted = settingsSaveData.GetValueOrDefault("MusicMuted");
        }

        private static Dictionary<string, bool>? LoadPuzzlesFromJson(string loadFileName) {
            bool saveFileExists = File.Exists(SAVE_DIRECTORY + loadFileName + ".json");
            if (!saveFileExists) {
                return null;
            }

            string allJsonText = File.ReadAllText(SAVE_DIRECTORY + loadFileName + ".json");
            Dictionary<string, bool>? puzzlesSaveData = JsonSerializer.Deserialize<Dictionary<string, bool>>(allJsonText);
            return puzzlesSaveData;
        }
        #endregion


        #region Resetting Save Data
        public static void ResetStandardPuzzlesSaveData() {
            Dictionary<string, bool> standardPuzzlesSaveData = new Dictionary<string, bool>();
            SaveDictionaryToJson(standardPuzzlesSaveData, "StandardPuzzlesSaveData");

            LoadStandardPuzzles();
        }

        public static void ResetBonusPuzzlesSaveData() {
            Dictionary<string, bool> bonusPuzzlesSaveData = new Dictionary<string, bool>();
            SaveDictionaryToJson(bonusPuzzlesSaveData, "BonusPuzzlesSaveData");

            LoadBonusPuzzles();
        }
        #endregion
    }
}
