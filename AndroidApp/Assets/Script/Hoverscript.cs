using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverscript : Singleton<Hoverscript>
{
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        MouseMovement();
    }
    void MouseMovement()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }
    public void Hoveractivation(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

}
