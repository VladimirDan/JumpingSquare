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
        public Button musicButton;
        public Sprite soundOnSprite;
        public Sprite soundOffSprite;
        public Sprite musicOnSprite;
        public Sprite musicOffSprite;
        
        private bool isSoundOn = true;
        private bool isMusicOn = true;
        private AudioManager _audioManager;
        private LevelManager levelManager;
        
        

        public void Initialize(AudioManager audioManager, LevelManager levelManager)
        {
            _audioManager = AudioManager.Instance;
            this.levelManager = levelManager;

            playButton.onClick.AddListener(StartGame);
            soundButton.onClick.AddListener(ToggleSound);
            musicButton.onClick.AddListener(ToggleMusic);
            
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
        }

        public void ToggleMusic()
        {
            isMusicOn = !isMusicOn;
            if (isMusicOn)
            {
                _audioManager.PlayMusic();
            }
            else
            {
                _audioManager.PauseMusic();
            }
            UpdateMusicButtonImage();
        }

        private void UpdateSoundButtonImage()
        {
            soundButton.GetComponent<Image>().sprite = isSoundOn ? soundOnSprite : soundOffSprite;
        }

        private void UpdateMusicButtonImage()
        {
            musicButton.GetComponent<Image>().sprite = isMusicOn ? musicOnSprite : musicOffSprite;
        }

        private void CheckSoundState()
        {
            isSoundOn = AudioListener.volume > 0f;
            isMusicOn = _audioManager.MusicIsPlaying;  
            UpdateSoundButtonImage();
            UpdateMusicButtonImage();
        }
    }
}
