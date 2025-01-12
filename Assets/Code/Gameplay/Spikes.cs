using UnityEngine;
using Code.Gameplay.Player;

namespace Code.Gameplay
{
    public class Spikes : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                if (player != null)
                {
                    KillPlayer(player);
                }
            }
        }

        private void KillPlayer(PlayerController player)
        {
            Debug.Log("Игрок погиб!");
            player.Die();
        }
    }
}