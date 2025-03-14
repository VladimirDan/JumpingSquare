using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Gameplay.Level
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }

        public LevelPrefabs levelPrefabs;
        private GameObject activeLevel;
        private int activeLevelIndex;

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
            int storedLevelIndex = LevelProgressManager.LoadLevelProgress(); 
            LoadLevel(storedLevelIndex); 
        }
        
        public void LoadLevel(int index)
        {
            if (activeLevel != null)
            {
                Destroy(activeLevel);
            }
            
            activeLevelIndex = index % levelPrefabs.levelPrefabs.Length; 
            GameObject selectedLevelPrefab = levelPrefabs.GetLevelPrefab(activeLevelIndex);
            if (selectedLevelPrefab != null)
            {
                activeLevel = Instantiate(selectedLevelPrefab);
            }
        }

        public int GetCurrentLevelIndex()
        {
            return activeLevelIndex; 
        }
    }
}