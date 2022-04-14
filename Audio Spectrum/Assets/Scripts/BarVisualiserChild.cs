using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarVisualiserChild : MonoBehaviour
{
    public float Value;
    public float InitWidth;
    public float InitHeight;

    public SpriteRenderer spriteRenderer;

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
        Vector3 scale = new Vector3(InitWidth, Value,1);
        transform.localScale = scale;
    }
}
