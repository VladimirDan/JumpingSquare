using UnityEngine;

namespace Code.Gameplay
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private Portal linkedPortal;
        [SerializeField] private float teleportCooldown = 0.5f;

        private bool isTeleporting;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!isTeleporting && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Teleport(collision.gameObject);
            }
        }

        private void Teleport(GameObject player)
        {
            if (linkedPortal != null)
            {
                linkedPortal.SetTeleporting();
                player.transform.position = linkedPortal.transform.position;
                Debug.Log("Игрок телепортирован в другой портал!");
            }
        }

        private void SetTeleporting()
        {
            isTeleporting = true;
            Invoke(nameof(ResetTeleporting), teleportCooldown);
        }

        private void ResetTeleporting()
        {
            isTeleporting = false;
        }
    }
}