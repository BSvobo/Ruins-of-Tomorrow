using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private float time;
    private float fade = 4.5f;
    private Color transparent;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        time = Time.time;
        sprite = GetComponent<SpriteRenderer>();
        //transparent = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - time > fade)
        {
            FadeSprite();
        }
        if (sprite.color.a < .1f)
        {
            destroy();
        }
    }

    void FadeSprite()
    {
        Color newColor = new Vector4(0f, 0f, 0f, .01f);
        sprite.color = sprite.color - newColor;
    }

    private void destroy()
    {
        Destroy(gameObject);
    }
}
