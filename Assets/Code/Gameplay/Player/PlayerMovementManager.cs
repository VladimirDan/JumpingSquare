using Code.SoundManager;
using Code.Gameplay.Player.PowerProjectileLogic;
using UnityEngine;

namespace Code.Gameplay.Player
{
    public class PlayerMovementManager : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D playerRigidbody;
        [SerializeField] private PlayerParameters playerParameters;
        [SerializeField] private GameObject powerProjectilePrefab;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private AudioManager audioManager;
        
        public void Initialize(AudioManager audioManager)
        {
            this.audioManager = audioManager;
        }
        
        public void ApplyForceToPlayer(Vector3 vector)
        {
            Vector3 force = vector.normalized * playerParameters.jumpPower;
            playerRigidbody.AddForce(force, ForceMode2D.Force);
        }

        public void EmitPowerProjectile(Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - shootPoint.position;
            GameObject powerProjectile = Instantiate(powerProjectilePrefab, shootPoint.position, Quaternion.identity);
            PowerProjectile projectile = powerProjectile.GetComponent<PowerProjectile>();

            if (projectile != null)
            {
                projectile.Initialize(direction, playerParameters.projectileSpeed, this, audioManager);
            }

            audioManager.PlayFireSound();
        }
    }
}

