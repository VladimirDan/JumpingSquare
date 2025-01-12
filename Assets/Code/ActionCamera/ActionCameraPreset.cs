using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts
{
    [Serializable]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ActionCameraPreset : ActionCameraSimplePreset
    {
        [SerializeField] private CameraFitSprite.FitType fitType = CameraFitSprite.FitType.Full;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float duration;
        [SerializeField] private Ease moveEase = Ease.InOutSine;
        [SerializeField] private ActionCamera.FollowToSideType followToSideType = ActionCamera.FollowToSideType.Center;

        public override CameraFitSprite.FitType FitType => fitType;
        public override Bounds Bounds => spriteRenderer.bounds;
        public override float Duration => duration;
        public override Ease MoveEase => moveEase;
        public override ActionCamera.FollowToSideType FollowToSideType => followToSideType;

        [ContextMenu(nameof(SaveCameraPos))]
        private void SaveFullCameraInfo()
        {
            SaveCameraPos();
        }

        [ContextMenu(nameof(SaveCameraPos))]
        private void SaveCameraPos() => spriteRenderer.transform.position = Camera.main.transform.position;
    }
}