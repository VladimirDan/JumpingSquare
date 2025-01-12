using UnityEngine;

public static class CameraExtensions
{
    #region Public Methods

    public static float GetHeight(this Camera camera) => 2f * camera.orthographicSize;

    public static float GetWidth(this Camera camera) => camera.GetHeight() * camera.aspect;

    public static Vector2 GetCameraSize(this Camera camera) => new Vector2(camera.GetWidth(), camera.GetHeight());
    
    public static Bounds GetBounds(this Camera camera, float zSize = 1f) =>
        new Bounds(camera.transform.position, new Vector3(camera.GetWidth(), camera.GetHeight(), zSize));
    
    public static bool IsVector2InCameraView(this Camera camera, Vector2 point)
    {
        Vector3 viewportPoint = camera.WorldToViewportPoint(point);
        return viewportPoint.x <= 1
               && viewportPoint.x >= 0
               && viewportPoint.y <= 1
               && viewportPoint.y >= 0;
    }

    public static bool IsRectFullyInCameraView(this Camera camera, Rect rect)
    {
        Vector2 cameraSize = camera.GetCameraSize();
        Vector2 positions = new Vector2(camera.transform.position.x - (cameraSize.x / 2),
            camera.transform.position.y - cameraSize.y / 2);

        Rect cameraRect = new Rect(positions, cameraSize);

        return cameraRect.Overlaps(rect);
    }

    public static RaycastHit2D[] GetRaycastHits2DMousePosition(this Camera camera)
    {
        Vector3 mousePositionWorldSpace = camera.ScreenToWorldPoint(Input.mousePosition);
        return Physics2D.RaycastAll(mousePositionWorldSpace, Vector2.zero);
    }
    
    public static Bounds GetPerspectiveBoundsByObject(this Camera camera, Transform transformObject)
    {
        return camera.GetPerspectiveBounds(Mathf.Abs(transformObject.position.z - camera.transform.position.z));
    }
    
    public static Bounds GetPerspectiveBounds(this Camera camera, float distance)
    {
        var bottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        var topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, distance));

        var bounds = new Bounds(camera.transform.position, Vector3.zero);

        bounds.Encapsulate(bottomLeft);
        bounds.Encapsulate(topRight);

        return bounds;
    }

    public static bool ObjectIsFullVisibleInCamera(this Camera camera, Bounds bounds) => camera.orthographic
        ? bounds.Intersects(camera.GetBounds(bounds.max.z))
        : GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), bounds);

    public static Vector3[] GetViewCorners(this Camera camera, float z)
    {
        int size = 4;

        Vector3[] corners = new Vector3[size];

        Transform cameraTransform = camera.transform;

        Bounds bounds = GetPerspectiveBounds(camera, Mathf.Abs(z - cameraTransform.position.z));

        Vector3 min = bounds.min;
        Vector3 max = bounds.max;

        corners[0] = new Vector3(min.x, min.y, z);
        corners[1] = new Vector3(min.x, max.y, z);
        corners[2] = new Vector3(max.x, max.y, z);
        corners[3] = new Vector3(max.x, min.y, z);

        return corners;
    }

    #endregion
}