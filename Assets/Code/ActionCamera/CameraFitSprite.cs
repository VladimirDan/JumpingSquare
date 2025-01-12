using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFitSprite : MonoBehaviour
{
    #region Fields

    [SerializeField, Tooltip("")]
    private SpriteRenderer _spriteRenderer = null;

    [SerializeField, Tooltip("")]
    private FitType _fitType = default;

    private Camera _camera = null;

    #endregion

    #region Event Functions

    private void Awake()
    {
        _camera = GetComponent<Camera>();

        FitSprite();
    }

    #endregion

    #region Private Methods

    private void FitSprite()
    {
        _camera.orthographicSize = GetTargetOrthoSize(_spriteRenderer, _fitType);
    }

    public static float GetTargetOrthoSize(SpriteRenderer sprite, FitType fitType = FitType.Horizontal)
    {
        var sprBoundsSize = sprite.bounds.size;

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = sprBoundsSize.x / sprBoundsSize.y;

        float differenceInSize = targetRatio / screenRatio;
            
        switch (fitType)
        {
            case FitType.FirstSmall:
                return differenceInSize > 1 ? sprBoundsSize.y / 2 : sprBoundsSize.y / 2 * differenceInSize;
            case FitType.Horizontal: 
                return sprBoundsSize.y / 2 * differenceInSize;
            case FitType.Vertical:
                return sprBoundsSize.y / 2;
            case FitType.Full:
                return differenceInSize > 1 ? sprBoundsSize.y / 2 * differenceInSize : sprBoundsSize.y / 2;
            default:
                throw new ArgumentOutOfRangeException(nameof(fitType), fitType, null);
        }
    }
    
    public static float GetTargetOrthoSize(SpriteRenderer sprite, float camAspect, FitType fitType = FitType.Horizontal)
    {
        var sprBoundsSize = sprite.bounds.size;
            
        var horizontalOrto = sprBoundsSize.x / camAspect / 2;
        var verticalOrto = sprBoundsSize.y / 2;
            
        return fitType switch
        {
            FitType.FirstSmall => horizontalOrto < verticalOrto ? horizontalOrto : verticalOrto,
            FitType.Full => horizontalOrto > verticalOrto ? horizontalOrto : verticalOrto,
            FitType.Horizontal => horizontalOrto,
            FitType.Vertical => verticalOrto,
            _ => throw new ArgumentOutOfRangeException(nameof(fitType), fitType, null)
        };
    }
    
    #endregion

    #region Nested

    public enum FitType
    {
        Full = 0,
        Horizontal = 1,
        Vertical = 2,
        FirstSmall = 3,
    }

    #endregion
}