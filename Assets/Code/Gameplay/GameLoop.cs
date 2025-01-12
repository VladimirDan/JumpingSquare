using UnityEngine;
using Code.Gameplay.Level;
using Code.Gameplay.Player;
using UnityEngine.SceneManagement;
using Code.SoundManager;

namespace Code.Gameplay
{
    public class GameLoop : MonoBehaviour
    {
        private GameObject player;
        private Collider2D finishZone;
        private LevelManager levelManager;
        private AudioManager audioManager;

        private bool isLevelCompleted = false;
        private bool isInitialized = false;

        public void Initialize(GameObject player, Collider2D finishZone, LevelManager levelManager, AudioManager audioManager)
        {
            this.player = player;
            this.finishZone = finishZone;
            this.levelManager = levelManager;
            this.audioManager = audioManager;
            isInitialized = true;
        }

        private void Update()
        {
            if (!isInitialized)
                return;

            if (IsPlayerInFinishZone())
            {
                CompleteLevel();
            }
            
            if (!player.GetComponent<PlayerController>().isAlive)
            {
                audioManager.PlayLoseSound();
                SceneManager.LoadScene("Game");
            }
        }

        private bool IsPlayerInFinishZone()
        {
            Collider2D playerCollider = player.GetComponent<Collider2D>();
            if (playerCollider != null && finishZone != null)
            {
                float playerArea = playerCollider.bounds.size.x * playerCollider.bounds.size.y;
                float finishArea = finishZone.bounds.size.x * finishZone.bounds.size.y;
                bool isInsideFinishZone = finishZone.bounds.Contains(playerCollider.bounds.center);
                if (isInsideFinishZone && playerArea * 0.5f <= finishArea)
                {
                    return true;
                }
            }
            return false;
        }

        private void CompleteLevel()
        {
            if (!isLevelCompleted)
            {
                isLevelCompleted = true;
                Debug.Log("Конец уровня: Игрок достиг финиша");
                audioManager.PlayVictorySound();

                if (levelManager != null)
                {
                    int nextLevelIndex = LevelProgressManager.LoadLevelProgress() + 1;
                    LevelProgressManager.SaveLevelProgress(nextLevelIndex);
                    SceneManager.LoadScene("Game");
                }
                else
                {
                    Debug.LogError("LevelManager не установлен!");
                }
            }
        }
    }
}
