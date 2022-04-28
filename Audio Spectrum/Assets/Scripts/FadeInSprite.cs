using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInSprite : MonoBehaviour
{
    [Range(0,5)]
    public float FadeInSpeed;
    public float Opacity = 0;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (!spriteRenderer)
        {
            Destroy(this.gameObject);
        }
    }
    public void SetOpacity(float Value)
    {
        Opacity = Value;
    }
    private void SetColour()
    {
        Color currentColour = spriteRenderer.color;
        spriteRenderer.color = new Color(
            currentColour.r,
            currentColour.g,
            currentColour.b,
            Opacity
            );
    }
    // Update is called once per frame
    void Update()
    {
        if (Opacity < 1)
        {
            Opacity += FadeInSpeed * Time.deltaTime;
            if (Opacity > 1)
            {
                Opacity = 1;
            }
        }

        SetColour();
    }
}
