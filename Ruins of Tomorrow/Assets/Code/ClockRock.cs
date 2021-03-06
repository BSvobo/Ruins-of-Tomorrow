using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockRock : MonoBehaviour
{
    public Sprite glowy;
    public Sprite dimmy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = glowy;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = dimmy;
        }
    }

}
