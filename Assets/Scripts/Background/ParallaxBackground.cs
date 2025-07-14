using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private Camera mainCamera;
    private float previousCameraX;
    private float cameraHalfWidth;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        InitializeLayers();
    }

    private void FixedUpdate()
    {
        float cameraX = mainCamera.transform.position.x;
        float deltaX = cameraX - previousCameraX;
        previousCameraX = cameraX;

        float cameraLeftBound = cameraX - cameraHalfWidth;
        float cameraRightBound = cameraX + cameraHalfWidth;

        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(deltaX);
            layer.LoopBackground(cameraLeftBound, cameraRightBound);
        }
    }

    private void InitializeLayers()
    {
        foreach (var layer in backgroundLayers)
            layer.CalulateImageWidth();
    }
}
