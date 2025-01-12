using UnityEngine;

namespace Code.Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        public bool isAlive = true;
        
        public void Die()
        {
            if (isAlive)
            {
                isAlive = false;
                Debug.Log("Игрок погиб!");
            }
        }
    }
}