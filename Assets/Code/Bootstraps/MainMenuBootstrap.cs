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
            audioManager.Initialize();
            audioManager.PlayMusic();
            levelManager.Initialize();
            mainMenuUIController.Initialize(audioManager, levelManager);
        }
    }
}