using UnityEngine;
using Code.Gameplay.Player;

namespace Code.InputManager
{
    public interface IInputHandler
    {
        void Initialize(PlayerMovementManager playerMovementManager);
        Vector3 GetInputPosition();
        bool IsTouchInGameArea(Vector3 position);
    }
}