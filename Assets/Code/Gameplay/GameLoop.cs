using UnityEngine;
using Code.Gameplay.Level;
using Code.Gameplay.Player;
using UnityEngine.SceneManagement;
using Code.SoundManager;

namespace Code.Gameplay
{
    public class GameLoop : MonoBehaviour
    {
        private GameObject playerObject;
        private Collider2D finishCollider;
        private LevelManager levelManager;
        private AudioManager soundManager;
        private PlayerController playerController;
        private Collider2D playerCollider;

        private bool levelCompleted = false;
        private bool initialized = false;

        public void Initialize(GameObject player, Collider2D finish, LevelManager levelMgr, AudioManager audioMgr)
        {
            if (player == null || finish == null || levelMgr == null || audioMgr == null)
            {
                Debug.LogError("GameLoop: Невозможно инициализировать - один из параметров null!");
                return;
            }

            playerObject = player;
            playerController = player.GetComponent<PlayerController>();
            playerCollider = player.GetComponent<Collider2D>();
            finishCollider = finish;
            levelManager = levelMgr;
            soundManager = audioMgr;
            initialized = true;
        }

        private void Update()
        {
            if (!initialized || levelCompleted || playerController == null)
                return;

            if (IsPlayerAtFinish())
            {
                CompleteLevel();
            }
            
            if (!playerController.isAlive)
            {
                soundManager.PlayLoseSound();
                SceneManager.LoadScene("Game");
            }
        }

        private bool IsPlayerAtFinish()
        {
            if (playerCollider == null || finishCollider == null)
                return false;

            return finishCollider.bounds.Contains(playerCollider.bounds.center);
        }

        private void CompleteLevel()
        {
            levelCompleted = true;
            Debug.Log("Конец уровня: Игрок достиг финиша");
            soundManager.PlayVictorySound();

            if (levelManager != null)
            {
                int nextLevel = LevelProgressManager.LoadLevelProgress() + 1;
                LevelProgressManager.SaveLevelProgress(nextLevel);
                SceneManager.LoadScene("Game");
            }
            else
            {
                Debug.LogError("GameLoop: LevelManager не установлен!");
            }
        }
    }
}
