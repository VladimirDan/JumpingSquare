using UnityEngine;

namespace Code.Gameplay.Player
{
    [CreateAssetMenu(
        fileName = "PlayerParameters",
        menuName = "Gameplay/Player Parameters",
        order = 0
    )]
    public class PlayerParameters : ScriptableObject
    {
        public float jumpPower;
        public float projectileSpeed;
    }
}