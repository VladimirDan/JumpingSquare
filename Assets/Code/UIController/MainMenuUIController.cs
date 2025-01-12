using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Code.SoundManager;
using Code.Gameplay.Level;

namespace Code.UIController
{
    public class MainMenuUIController : MonoBehaviour
    {
        public Button playButton;
        public Button soundButton;
        public Sprite soundOnSprite;
        public Sprite soundOffSprite;
        private bool isSoundOn = true;
        private AudioManager _audioManager;
        private LevelManager levelManager;

        public void Initialize(AudioManager audioManager, LevelManager levelManager)
        {
            this._audioManager = audioManager;
            this.levelManager = levelManager;
            playButton.onClick.AddListener(StartGame);
            soundButton.onClick.AddListener(ToggleSound);
            UpdateSoundButtonImage();
            CheckSoundState();
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Game");
        }

        public void ToggleSound()
        {
            isSoundOn = !isSoundOn;
            AudioListener.volume = isSoundOn ? 1f : 0f;
            UpdateSoundButtonImage();

            if (isSoundOn && _audioManager != null)
            {
                _audioManager.PlayMusic();
            }
            else if (_audioManager != null)
            {
                _audioManager.PauseMusic();
            }
        }

        private void UpdateSoundButtonImage()
        {
            soundButton.GetComponent<Image>().sprite = isSoundOn ? soundOnSprite : soundOffSprite;
        }

        private void CheckSoundState()
        {
            isSoundOn = AudioListener.volume > 0f;
            UpdateSoundButtonImage();
        }
    }
}