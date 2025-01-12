using DG.Tweening;
using UnityEngine;

namespace Game.Scripts
{
    public abstract class ActionCameraSimplePreset : MonoBehaviour
    {
        public abstract CameraFitSprite.FitType FitType { get; }
        public abstract Bounds Bounds { get; }
        public abstract float Duration { get; }
        public abstract Ease MoveEase { get; }
        public abstract ActionCamera.FollowToSideType FollowToSideType { get; }
    }
}