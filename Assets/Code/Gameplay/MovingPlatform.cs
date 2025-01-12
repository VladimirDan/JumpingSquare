using UnityEngine;

namespace Code.Gameplay
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private float distance = 2f;
        [SerializeField] private float speed = 2f;
        [SerializeField] private Direction movementDirection = Direction.Horizontal;

        private Vector2 startPosition;
        private Vector2 endPosition;
        private bool movingToEnd = true;

        private enum Direction
        {
            Horizontal,
            Vertical
        }

        private void Start()
        {
            startPosition = transform.position;

            if (movementDirection == Direction.Horizontal)
            {
                endPosition = startPosition + Vector2.right * distance;
            }
            else
            {
                endPosition = startPosition + Vector2.up * distance;
            }
        }

        private void Update()
        {
            MovePlatform();
        }

        private void MovePlatform()
        {
            if (movingToEnd)
            {
                transform.position = Vector2.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, endPosition) < 0.1f)
                {
                    movingToEnd = false;
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, startPosition) < 0.1f)
                {
                    movingToEnd = true;
                }
            }
        }
    }
}