using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Grombcross.Models.Systems {
    public static class SaveSystem {
        const string SAVE_DIRECTORY = "SaveData/";



        // Saving game
        public static void SaveGame() {
            SaveStandardPuzzles();
            SaveBonusPuzzles();
        }
        private static void SaveStandardPuzzles() {
            Dictionary<string, bool> standardPuzzlesSaveData = new Dictionary<string, bool>();
            foreach (Puzzle p in GlobalVariables.StandardPuzzles) {
                standardPuzzlesSaveData.Add(p.Name, p.Completed);
            }

            SavePuzzlesToJson(standardPuzzlesSaveData, "StandardPuzzlesSaveData");
        }
        private static void SaveBonusPuzzles() {
            Dictionary<string, bool> bonusPuzzlesSaveData = new Dictionary<string, bool>();
            foreach (Puzzle p in GlobalVariables.BonusPuzzles) {
                bonusPuzzlesSaveData.Add(p.Name, p.Completed);
            }

            SavePuzzlesToJson(bonusPuzzlesSaveData, "BonusPuzzlesSaveData");
        }

        private static void SavePuzzlesToJson(Dictionary<string, bool> puzzlesSaveData, string saveFileName) {
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions() { WriteIndented = true };
            string json = JsonSerializer.Serialize(puzzlesSaveData, jsonOptions); ;

            Directory.CreateDirectory(SAVE_DIRECTORY);
            File.WriteAllText(SAVE_DIRECTORY + saveFileName + ".json", json);
        }



        // Loading game
        public static void LoadGame() {
            LoadStandardPuzzles();
            LoadBonusPuzzles();
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
            if (bonusPuzzlesSaveData != null) {
                foreach (Puzzle p in GlobalVariables.BonusPuzzles) {
                    p.Completed = bonusPuzzlesSaveData.GetValueOrDefault(p.Name);
                }
            }
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



        // Reseting Save Data
        public static void ResetStandardPuzzlesSaveData() {
            Dictionary<string, bool> standardPuzzlesSaveData = new Dictionary<string, bool>();
            SavePuzzlesToJson(standardPuzzlesSaveData, "StandardPuzzlesSaveData");

            LoadStandardPuzzles();
        }

        public static void ResetBonusPuzzlesSaveData() {
            Dictionary<string, bool> bonusPuzzlesSaveData = new Dictionary<string, bool>();
            SavePuzzlesToJson(bonusPuzzlesSaveData, "BonusPuzzlesSaveData");

            LoadBonusPuzzles();
        }
    }
}
