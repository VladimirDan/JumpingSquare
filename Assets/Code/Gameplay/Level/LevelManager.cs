using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Gameplay.Level
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }

        public LevelPrefabs levelPrefabs;
        private GameObject currentLevel;
        private int currentLevelIndex;

        public void Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void LoadCurrentLevel()
        {
            int savedLevelIndex = LevelProgressManager.LoadLevelProgress(); 
            LoadLevel(savedLevelIndex); 
        }
        
        public void LoadLevel(int index)
        {
            if (currentLevel != null)
            {
                Destroy(currentLevel);
            }
            
            currentLevelIndex = index % levelPrefabs.levelPrefabs.Length; 
            GameObject levelPrefab = levelPrefabs.GetLevelPrefab(currentLevelIndex);
            if (levelPrefab != null)
            {
                currentLevel = Instantiate(levelPrefab);
            }
        }

        public int GetCurrentLevelIndex()
        {
            return currentLevelIndex; 
        }
    }
}