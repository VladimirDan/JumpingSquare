using DG.Tweening;
using UnityEngine;

namespace Game.Scripts
{
    public class ActionCameraDoublePreset : ActionCameraSimplePreset
    {
        [SerializeField] private SpriteRenderer mainZoneSprite;
        [Space]
        [SerializeField] private ActionCameraSimplePreset minPreset;
        [SerializeField] private ActionCameraSimplePreset maxPreset;

        private ActionCameraSimplePreset ActualPreset { get; set; }

        public override CameraFitSprite.FitType FitType => ActualPreset.FitType;
        public override Bounds Bounds => ActualPreset.Bounds;
        public override float Duration => ActualPreset.Duration;
        public override Ease MoveEase => ActualPreset.MoveEase;
        public override ActionCamera.FollowToSideType FollowToSideType => ActualPreset.FollowToSideType;

        public void CalculateActualPreset(ActionCamera actionCamera)
        {
            var minPresetBounds = GetBounds(actionCamera, minPreset);
            
            ActualPreset = CheckBounds(mainZoneSprite.bounds, minPresetBounds) ? minPreset : maxPreset;
        }

        private static bool CheckBounds(Bounds main, Bounds secondary)
        {
            return main.Contains(new Vector3(secondary.min.x, secondary.min.y, main.min.z)) &&
                   main.Contains(new Vector3(secondary.max.x, secondary.max.y, main.max.z));
        }

        private static Bounds GetBounds(ActionCamera actionCamera, ActionCameraSimplePreset preset)
        {
            var calculateOrto = actionCamera.GetOrthoSizeByBounds(preset.Bounds, preset.FitType);
            
            var calculatePos =
                actionCamera.GetPosByFollowSideWithOrto(preset.Bounds, preset.FollowToSideType, calculateOrto);

            var halfWidth = calculateOrto * actionCamera.Camera.aspect;

            return new Bounds(calculatePos, new Vector3(2f * halfWidth, 2f * calculateOrto, 0f));
        }
    }
}