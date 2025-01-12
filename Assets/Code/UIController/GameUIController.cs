using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Code.Gameplay.Level;
using TMPro;

namespace Code.UIController
{
    public class GameUIController : MonoBehaviour
    {
        public Button restartButton;
        public Button mainMenuButton;
        private LevelManager levelManager;
        public TextMeshProUGUI levelIndexText;

        public void Initialize(LevelManager levelManager)
        {
            this.levelManager = levelManager;
            
            restartButton.onClick.AddListener(RestartGame);
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
            
            UpdateLevelIndexText();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene("Game");
        }

        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void UpdateLevelIndexText()
        {
            if (levelManager != null && levelIndexText != null)
            {
                levelIndexText.text = $"Level {levelManager.GetCurrentLevelIndex() + 1}";
            }
            else
            {
                Debug.LogWarning("LevelManager or levelIndexText is not assigned.");
            }
        }
    }
}