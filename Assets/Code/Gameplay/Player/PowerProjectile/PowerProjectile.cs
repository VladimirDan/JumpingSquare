using UnityEngine;
using Code.SoundManager;

namespace Code.Gameplay.Player.PowerProjectileLogic
{
    public class PowerProjectile : MonoBehaviour
    {
        private Vector3 movementDirection;
        private float projectileSpeed;
        private PlayerMovementManager movementManager;
        private AudioManager soundManager;
        private ParticleSystem explosionEffect;
        private Rigidbody2D rb;
        private Collider2D collider;
        private SpriteRenderer spriteRenderer;

        public void Initialize(Vector3 direction, float speed, PlayerMovementManager playerMovement, AudioManager audio)
        {
            movementDirection = direction.normalized;
            projectileSpeed = speed;
            movementManager = playerMovement;
            soundManager = audio;
            explosionEffect = transform.Find("ProjectileExplosion")?.GetComponent<ParticleSystem>();

            rb = GetComponent<Rigidbody2D>();
            collider = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            transform.position += movementDirection * projectileSpeed * Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                Vector3 reflectedForce = -movementDirection;
                movementManager?.ApplyForceToPlayer(reflectedForce);
            }

            explosionEffect?.Play();
            soundManager?.PlayExplosionSound();

            if (rb) Destroy(rb);
            if (collider) Destroy(collider);
            if (spriteRenderer) Destroy(spriteRenderer);

            Transform projectileTrail = transform.Find("ProjectileFlying");
            if (projectileTrail) Destroy(projectileTrail.gameObject);

            Destroy(gameObject, 5f);
        }
    }
}