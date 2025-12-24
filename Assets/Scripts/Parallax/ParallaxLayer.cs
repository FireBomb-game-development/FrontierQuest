using UnityEngine;
[System.Serializable]
public class ParallaxLayer
{

    [SerializeField] private Transform Background;
    [SerializeField] private float parallaxMultiplier;
    [SerializeField] private float imageWidthOffset =10;

    private float imageFullWidth;
    private float imageHalfWidth;

    public void CalcuateImageWidth()
    {
        imageFullWidth = Background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2;
    }

    public void Move(float distanceToMove)
    {
        Background.position += Vector3.right * (distanceToMove * parallaxMultiplier);
        
    }

    public void LoopBackground(float cameraLeftEdge, float cameraRightEdgh)
    {
        float imageRightEdge = (Background.position.x + imageHalfWidth) -imageWidthOffset;
        float imageLeftEdge = (Background.position.x - imageHalfWidth) + imageWidthOffset;

        if (imageRightEdge < cameraLeftEdge)
        {
            Background.position += Vector3.right * imageFullWidth;
            Debug.Log("move background by" + Vector3.right * imageFullWidth);
            Debug.Log("camera right edge is:" + cameraRightEdgh);
            Debug.Log("image rigt edhe is:" + imageRightEdge);
        }
        else if (imageLeftEdge > cameraRightEdgh)
        {
            Background.position += Vector3.right * -imageFullWidth;
            Debug.Log("move background by" + Vector3.right * -imageFullWidth);
            Debug.Log("camera left edge is:" + cameraLeftEdge);
            Debug.Log("image rigt edhe is:" + imageLeftEdge);
        }

    }


}
