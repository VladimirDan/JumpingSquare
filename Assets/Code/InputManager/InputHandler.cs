using UnityEngine;
using Code.Gameplay.Player;

namespace Code.InputManager
{
    public abstract class InputHandler : IInputHandler
    {
        public abstract void Initialize(PlayerMovementManager playerMovementManager);
        public abstract Vector3 GetInputPosition();
        public abstract bool IsTouchInGameArea(Vector3 position);
    }
}