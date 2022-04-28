using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGFillSprite : MonoBehaviour
{
    public GameObject MainCam;
    public Sprite targetSprite;
    public Vector3 ScreenSize;
    public SpriteRenderer spriteRenderer;
    public Vector3 initSpriteBoundSize;
    public Vector3 spriteBoundSize;


    public void GetScreenSize()
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

    public void UpdateInfo()
    {
        spriteBoundSize = spriteRenderer.bounds.size;
    }

    // Start is called before the first frame update
    void Start()
    {
        MainCam = GameObject.FindGameObjectWithTag("MainCamera");

        spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 1);
        spriteRenderer.sprite = targetSprite;
        spriteRenderer.sortingOrder = -100;

        initSpriteBoundSize = spriteRenderer.bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInfo();
        GetScreenSize();
        Vector3 targetSize = new Vector3(
            Screen.width,
            Screen.height
            );
        float ScreenArea = ScreenSize.x * ScreenSize.y;
        float SpriteArea = initSpriteBoundSize.x * initSpriteBoundSize.y;
        float targetArea = ScreenArea * 0.001f;
        float scale = 1;
        if (SpriteArea < targetArea)
        {
            scale = targetArea / SpriteArea;
        }


        transform.position = new Vector3(
            MainCam.transform.position.x,
            MainCam.transform.position.y,
            0
            );
        transform.localScale = new Vector3(scale, scale, 1);

    }
}
