using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public Rigidbody2D _rb;
    private bool BeingPushed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _rb.angularVelocity = 0;

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            BeingPushed = false;
        }

        else if (collision.gameObject.tag == "Player")
        {
            BeingPushed = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                BeingPushed = false;
            }
            else
            {
                BeingPushed = true;
            }
        }  
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BeingPushed = false;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("IN PULL RANGE");
            if (Input.GetKey(KeyCode.E))
            {
                _rb.velocity = col.attachedRigidbody.velocity *.9f;
            }
            else if (Input.GetKey(KeyCode.E) == false && !BeingPushed)
            {
                _rb.velocity = _rb.velocity = Vector2.zero;
            }
        }
    }
}
