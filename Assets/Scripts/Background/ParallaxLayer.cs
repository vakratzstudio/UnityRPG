using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplier;
    [SerializeField] private float backgroundImageWidthOffset = 10;

    private float imageWidth;

    public void CalulateImageWidth()
    {
        imageWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    public void Move(float distanceToMove)
    {
        background.position += Vector3.right * (distanceToMove * parallaxMultiplier);
    }

    public void LoopBackground(float cameraLeftBound, float cameraRightBound)
    {
        float imageRightEdge = (background.position.x + imageWidth / 2)
                              - backgroundImageWidthOffset;
        float imageLeftEdge = (background.position.x - imageWidth / 2)
                             + backgroundImageWidthOffset;

        if (imageRightEdge < cameraLeftBound)
            background.position += imageWidth * Vector3.right;
        else if (imageLeftEdge > cameraRightBound)
            background.position -= imageWidth * Vector3.right;
    }

}
