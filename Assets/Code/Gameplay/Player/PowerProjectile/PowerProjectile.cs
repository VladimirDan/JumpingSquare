using UnityEngine;
using Code.SoundManager;

namespace Code.Gameplay.Player.PowerProjectileLogic
{
    public class PowerProjectile : MonoBehaviour
    {
        private Vector3 flightDirection;
        private float speed;
        private PlayerMovementManager playerMovementManager;
        private AudioManager audioManager;
        public ParticleSystem explosionParticles; 

        public void Initialize(Vector3 direction, float speed, PlayerMovementManager movementManager, AudioManager audioManager)
        {
            this.flightDirection = direction.normalized;
            this.speed = speed;
            this.playerMovementManager = movementManager;
            this.audioManager = audioManager;
            explosionParticles = transform.Find("ProjectileExplosion").GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            transform.position += flightDirection * speed * Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                Vector3 oppositeForce = -flightDirection;
                playerMovementManager?.ApplyForceToPlayer(oppositeForce);
            }
            
            explosionParticles.Play();
            audioManager?.PlayExplosionSound();
            
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<Collider2D>());
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(transform.Find("ProjectileFlying").gameObject);
            //Destroy(gameObject);
        }
    }
}