using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPositionX;
    private float cameraHalfWidth;
    [SerializeField] private ParallaxLayer[] backgroundLayers;



    public void Awake()
    {
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        CalculateImageLength();
    }

    public void Update()
    {
        float currentCameraPositionX = mainCamera.transform.position.x;
        float distanceToMove = currentCameraPositionX - lastCameraPositionX;
        lastCameraPositionX = currentCameraPositionX;

        float cameraRightEdgh = currentCameraPositionX + cameraHalfWidth;
        float cameraLeftEdge = currentCameraPositionX - cameraHalfWidth;

        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(distanceToMove);
            layer.LoopBackground(cameraLeftEdge, cameraRightEdgh);
        }
    }


    private void CalculateImageLength()
    {
        foreach (ParallaxLayer layer in backgroundLayers) layer.CalcuateImageWidth();
    }
}
