using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts
{
    [RequireComponent(typeof(Camera))]
    public class ActionCamera : MonoBehaviour
    {
        [SerializeField] private new Camera camera;

        private Sequence _zoomSequence;
        
        private float _startOrthographicSize;
        private Vector3 _startPosition;
        
        private TaskCompletionSource<object> _tcs;

        public Camera Camera => camera;

        public float CameraHeight => camera.GetHeight();
        public float CameraWidth => camera.GetWidth();

        public float GetHeightByOrto(float ortoSize) => 2f * ortoSize;
        public float GetWidthByOrto(float ortoSize) => GetHeightByOrto(ortoSize) * camera.aspect;

        public float CameraOrtoSize => camera.orthographicSize;

        private void Start()
        {
            _startOrthographicSize = CameraOrtoSize;
            _startPosition = transform.position;
        }

        public void BreakAllActions()
        {
            CompleteAsyncOperations();
            _zoomSequence?.Kill();
        }

        public void Zoom(float endCameraSize, float duration, Action onZoomCallback = null)
        {
            BreakAllActions();
            
            _zoomSequence = DOTween.Sequence()
                .Append(camera.DOOrthoSize(endCameraSize, duration))
                .OnComplete(() => onZoomCallback?.Invoke());
        }

        public void ZoomToPoint(float ortoSize, float duration, Vector3 targetPos, Ease ease = Ease.Linear, Action callback = null)
        {
            BreakAllActions();
            
            _zoomSequence = DOTween.Sequence()
                .Append(camera.DOOrthoSize(ortoSize, duration).SetEase(ease))
                .Join(camera.transform.DOMove(new Vector3(targetPos.x, targetPos.y, camera.transform.position.z),
                    duration).SetEase(ease))
                .OnComplete(() => callback?.Invoke());
        }

        public void MoveByPreset(ActionCameraSimplePreset preset, Action callback = null)
        {
            if (preset is ActionCameraDoublePreset doublePreset)
            {
                doublePreset.CalculateActualPreset(this);
            }

            var targetOrtoSize = GetOrthoSizeByBounds(preset.Bounds, preset.FitType);
            
            var targetPosition = GetPosByFollowSideWithOrto(preset.Bounds, preset.FollowToSideType, targetOrtoSize);

            ZoomToPoint(targetOrtoSize, preset.Duration, targetPosition, preset.MoveEase, callback);
        }

        public void SetPresetImmediate(ActionCameraSimplePreset preset)
        {
            if (preset is ActionCameraDoublePreset doublePreset)
                doublePreset.CalculateActualPreset(this);

            var ortoSize = GetOrthoSizeByBounds(preset.Bounds, preset.FitType);

            camera.orthographicSize = ortoSize;
            
            transform.position = GetPosByFollowSideWithOrto(preset.Bounds, preset.FollowToSideType, ortoSize);
        }
        
        public Task MoveByPresetAsync(ActionCameraSimplePreset preset)
        {
            CompleteAsyncOperations();

            _tcs = new TaskCompletionSource<object>();
            MoveByPreset(preset, callback: CompleteAsyncOperations);

            return _tcs.Task;
        }

        public void ResetZoomToStart(float duration, Action completeCallback = null)
        {
            ZoomToPoint(_startOrthographicSize, duration, _startPosition, callback: completeCallback);
        }

        public void FollowToSpriteSide(SpriteRenderer sprite, FollowToSideType followToSideType)
        {
            camera.transform.position = FollowToSpriteSidePos(sprite, followToSideType);
        }

        public Vector3 FollowToSpriteSidePos(SpriteRenderer sprite, FollowToSideType followToSideType)
        {
            return GetPosByFollowSideWithOrto(sprite.bounds, followToSideType, CameraOrtoSize);
        }

        public float GetOrthoSizeByBounds(Bounds bounds, CameraFitSprite.FitType fitType)
        {
            var horizontalOrto = bounds.size.x / camera.aspect / 2;
            var verticalOrto = bounds.size.y / 2;

            return fitType switch
            {
                CameraFitSprite.FitType.FirstSmall => horizontalOrto < verticalOrto ? horizontalOrto : verticalOrto,
                CameraFitSprite.FitType.Full => horizontalOrto > verticalOrto ? horizontalOrto : verticalOrto,
                CameraFitSprite.FitType.Horizontal => horizontalOrto,
                CameraFitSprite.FitType.Vertical => verticalOrto,
                _ => throw new ArgumentOutOfRangeException(nameof(fitType), fitType, null)
            };
        }
        
        public Vector3 GetPosByFollowSideWithOrto(Bounds bounds, FollowToSideType followToSideType, float camOrto)
        {
            var camPos = transform.position;
            
            var camHeight = GetHeightByOrto(camOrto);
            var camWidth = GetWidthByOrto(camOrto);

            Vector2 followPos = followToSideType switch
            {
                FollowToSideType.Center => bounds.center,

                FollowToSideType.Left => new Vector2(bounds.min.x + camWidth / 2, camPos.y),
                FollowToSideType.Right => new Vector2(bounds.max.x - camWidth / 2, camPos.y),
                FollowToSideType.Top => new Vector2(camPos.x, bounds.max.y - camHeight / 2),
                FollowToSideType.Bottom => new Vector2(camPos.x, bounds.min.y + camHeight / 2),

                FollowToSideType.TopLeft => new Vector2(bounds.min.x + camWidth / 2, bounds.max.y - camHeight / 2),
                FollowToSideType.TopCenter => new Vector2(bounds.center.x, bounds.max.y - camHeight / 2),
                FollowToSideType.TopRight => new Vector2(bounds.max.x - camWidth / 2, bounds.max.y - camHeight / 2),

                FollowToSideType.BottomRight => new Vector2(bounds.max.x - camWidth / 2, bounds.min.y + camHeight / 2),
                FollowToSideType.BottomCenter => new Vector2(bounds.center.x, bounds.min.y + camHeight / 2),
                FollowToSideType.BottomLeft => new Vector2(bounds.min.x + camWidth / 2, bounds.min.y + camHeight / 2),

                FollowToSideType.LeftCenter => new Vector2(bounds.min.x + camWidth / 2, bounds.center.y),
                FollowToSideType.RightCenter => new Vector2(bounds.max.x - camWidth / 2, bounds.center.y),

                _ => throw new ArgumentOutOfRangeException(nameof(followToSideType), followToSideType, null)
            };

            return new Vector3(followPos.x, followPos.y, camera.transform.position.z);
        }

        private void CompleteAsyncOperations() => _tcs?.TrySetResult(null);

        public enum FollowToSideType
        {
            Center = 0,
            Left = 1,
            Right = 2,
            Top = 3,
            Bottom = 4,

            TopLeft = 5,
            TopCenter = 6,
            TopRight = 7,

            BottomRight = 8,
            BottomCenter = 9,
            BottomLeft = 10,

            LeftCenter = 11,
            RightCenter = 12,
        }
    }
}