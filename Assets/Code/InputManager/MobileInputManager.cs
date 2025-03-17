using UnityEngine;
 using Code.Gameplay.Player;
 
 namespace Code.InputManager
 {
     public class MobileInputManager : MonoBehaviour, IInputHandler
     {
         private PlayerMovementManager playerMovementManager;
         [SerializeField] private RectTransform gameArea;
 
         private bool isInitialized = false;
 
         public void Initialize(PlayerMovementManager playerMovementManager)
         {
             this.playerMovementManager = playerMovementManager;
             isInitialized = true;
         }
 
         void Update()
         {
             if (!isInitialized)
                 return;
 
             if (Input.touchCount > 0) // Проверяем наличие касаний
             {
                 Touch touch = Input.GetTouch(0); // Получаем первое касание
                 if (touch.phase == TouchPhase.Began) // Проверяем фазу касания
                 {
                     Vector3 touchPosition = touch.position;
                     touchPosition.z = Camera.main.transform.position.z * -1; // Используем позицию камеры для Z
 
                     Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                     worldPosition.z = 0f;
 
                     if (IsTouchInGameArea(worldPosition))
                     {
                         playerMovementManager.EmitPowerProjectile(worldPosition);
                     }
                 }
             }
         }
 
         public Vector3 GetInputPosition()
         {
             if (Input.touchCount > 0)
             {
                 Touch touch = Input.GetTouch(0);
                 if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                 {
                     Vector3 touchPosition = touch.position;
                     touchPosition.z = Camera.main.transform.position.z;
                     Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                     worldPosition.z = 0f;
                     return worldPosition;
                 }
             }
             return Vector3.zero;
         }
 
         public bool IsTouchInGameArea(Vector3 position)
         {
             Vector3[] corners = new Vector3[4];
             gameArea.GetWorldCorners(corners);
 
             float minX = corners[0].x;
             float maxX = corners[2].x;
             float minY = corners[0].y;
             float maxY = corners[2].y;
 
             return position.x >= minX && position.x <= maxX && position.y >= minY && position.y <= maxY;
         }
     }
 }