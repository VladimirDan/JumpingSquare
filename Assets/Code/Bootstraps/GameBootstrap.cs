using Code.Gameplay;
using Code.UIController;
using Code.Gameplay.Player;
using UnityEngine;
using Code.Gameplay.Level;
using Code.InputManager;
using Code.SoundManager;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameUIController gameUIController;
    private LevelManager levelManager;
    [SerializeField] private GameLoop gameLoop;
    [SerializeField] private MobileInputManager inputHandler;

    void Awake()
    {
        if (levelManager == null)
        {
            levelManager = LevelManager.Instance;
        }

        levelManager.LoadCurrentLevel();
        gameUIController.Initialize(levelManager);
        
        GameObject playerObject = GameObject.Find("Player");
        GameObject finishObject = GameObject.Find("Finish");

        PlayerMovementManager playerMovementManager = playerObject.GetComponent<PlayerMovementManager>();
        playerMovementManager.Initialize(AudioManager.Instance);
        inputHandler.Initialize(playerMovementManager);
        gameLoop.Initialize(playerObject, finishObject.GetComponent<Collider2D>(), levelManager, AudioManager.Instance);
        
    }
}