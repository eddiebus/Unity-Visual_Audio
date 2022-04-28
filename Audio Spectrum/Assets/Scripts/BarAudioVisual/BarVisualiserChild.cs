using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarVisualiserChild : MonoBehaviour
{
    public float Value;
    public float InitWidth;
    public float InitHeight;
    public float MaxHeight;

    public SpriteRenderer spriteRenderer;

    public void SetMaxHeight(float Value)
    {
        MaxHeight = Value;
    }

    public void SetValue(float Value)
    {
        this.Value = Value;
    }

    public void SetColour(Color newColour)
    {
        spriteRenderer.color = newColour;
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        InitWidth = spriteRenderer.bounds.size.x;
        InitHeight = spriteRenderer.bounds.size.y;
    }
    
    // Update is called once per frame
    void Update()
    {
        float newHeight = Value;
        if (newHeight > MaxHeight)
        {
            newHeight = MaxHeight;
        }
        Vector3 scale = new Vector3(InitWidth, newHeight,1);
        
        transform.localScale = scale;
    }
}
