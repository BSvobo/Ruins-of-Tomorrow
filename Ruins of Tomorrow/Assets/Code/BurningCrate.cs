using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningCrate : MonoBehaviour
{

    private SpriteRenderer sprite;
    //private Vector4 newcolor;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        //newColor = new Vector4(0.01f, 0.01f, 0.01f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BurnCrate()
    {
        Color newColor = new Vector4(0.01f, 0.01f, 0.01f, 1f);
        sprite.color = sprite.color - newColor;
    }

    public void DestroyObject()
    {
        //gameObject.destroy();
    }


}
