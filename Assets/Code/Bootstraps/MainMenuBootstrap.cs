using Code.UIController;
using Code.Gameplay.Level;
using UnityEngine;
using Code.SoundManager;
using UnityEngine.Serialization;

namespace Code.Bootstraps
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        [SerializeField] private MainMenuUIController mainMenuUIController;
        [FormerlySerializedAs("musicManager")] [SerializeField] private AudioManager audioManager;
        [SerializeField] private LevelManager levelManager;

        void Awake()
        {
            if (AudioManager.Instance == null)
            {
                Debug.Log("dddd");
                audioManager.Initialize();
                if (!AudioManager.Instance.MusicIsPlaying) 
                {
                    AudioManager.Instance.PlayMusic();
                    Debug.Log("PlayMusic");
                }
            }

            levelManager.Initialize();
            mainMenuUIController.Initialize(AudioManager.Instance, levelManager);
        }
    }
}