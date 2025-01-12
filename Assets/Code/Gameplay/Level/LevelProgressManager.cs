using System.IO;
using UnityEngine;

namespace Code.Gameplay.Level
{
    public static class LevelProgressManager
    {
        private static readonly string SaveFilePath = Path.Combine(Application.persistentDataPath, "level_progress.json");

        public static void SaveLevelProgress(int levelIndex)
        {
            var data = new LevelProgressData { CurrentLevelIndex = levelIndex };
            EnsureFileExists();
            File.WriteAllText(SaveFilePath, JsonUtility.ToJson(data));
        }

        public static int LoadLevelProgress()
        {
            EnsureFileExists();
            var json = File.ReadAllText(SaveFilePath);
            var data = JsonUtility.FromJson<LevelProgressData>(json);
            return data.CurrentLevelIndex;
        }

        private static void EnsureFileExists()
        {
            if (!File.Exists(SaveFilePath))
            {
                var defaultData = new LevelProgressData { CurrentLevelIndex = 0 };
                File.WriteAllText(SaveFilePath, JsonUtility.ToJson(defaultData));
            }
        }

        [System.Serializable]
        private class LevelProgressData
        {
            public int CurrentLevelIndex;
        }
    }
}