using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteToScreen : MonoBehaviour
{
    public Vector3 ScreenSize;
    public Sprite TargetSprite;
    public Color spriteColour;

    [Range (0,1)]
    public float xPos = 0;
    [Range(0, 1)]
    public float yPos = 0;

    private void GetSize()
    {
        Vector3 bottomLeftCorner = Camera.main.ScreenToWorldPoint(
            new Vector3(0, 0, 0)
            );

        Vector3 topRightCorner = Camera.main.ScreenToWorldPoint(
            new Vector3(
                Camera.main.pixelWidth,
                Camera.main.pixelHeight,
                0)
            );

        ScreenSize = new Vector3(
            topRightCorner.x - bottomLeftCorner.x,
            topRightCorner.y - bottomLeftCorner.y,
            0
            );
    }

    public void SetPosition()
    {
        float x = 0 - (ScreenSize.x / 2);
        float y = 0 - (ScreenSize.y / 2);

        x += ScreenSize.x * xPos;
        y += ScreenSize.y * yPos;
        transform.position = new Vector3(x, y, 0);

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GetSize();
        SetPosition();
    }
}
