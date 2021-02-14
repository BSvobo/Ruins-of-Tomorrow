using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rb;
    public float speed;
    private Timeable _timeable;

    enum direction {left, right, up, down};
    private direction _dir;

    private bool moving;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = new Vector2(0, 0);
        _dir = direction.right;
        moving = false;

        Screen.SetResolution(1280, 720, false);
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown("space"))
        {
            ClockRock();
        }
        HandleInputs();
    }

    void HandleInputs()
    {
        if (moving)
        {
            Move();
        }
        else
        {
          SetDirection();
        }
    }

    void SetDirection()
    {
        if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
        {
            _dir = direction.left;
           // print("left");
            moving = true;
        }
        else if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
        {
            _dir = direction.right;
           // print("right");
            moving = true;
        }
        else if (Input.GetKeyDown("up") || Input.GetKeyDown("w"))
        {
            _dir = direction.up;
            //print("up");
            moving = true;
        }
        else if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
        {
            _dir = direction.down;
            //print("down");
            moving = true;
        }
    }
    void Move()
    {
        if (_dir == direction.left)
        {
            _rb.velocity = new Vector2(-1,0) *speed;
            if (Input.GetKeyUp("left") || Input.GetKeyUp("a"))
            {
                moving = false;
                _rb.velocity = Vector2.zero;
            }
        }
        else if (_dir == direction.right)
        {
            _rb.velocity = new Vector2(1,0) *speed;
            if (Input.GetKeyUp("right") || Input.GetKeyUp("d"))
            {
                moving = false;
                _rb.velocity = Vector2.zero;
            }
        }
        else if (_dir == direction.up)
        {
            _rb.velocity = new Vector2(0,1) *speed;
            if (Input.GetKeyUp("up") || Input.GetKeyUp("w"))
            {
                moving = false;
                _rb.velocity = Vector2.zero;
            }
        }
        else if (_dir == direction.down)
        {
            _rb.velocity = new Vector2(0, -1) *speed;
            if (Input.GetKeyUp("down") || Input.GetKeyUp("s"))
            {
                moving = false;
                _rb.velocity = Vector2.zero;
            }
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crate")
        {
            Debug.Log("The player collided with the crate");
            if (_dir == direction.left)
            {
                collision.rigidbody.velocity = new Vector2(-1, 0) * speed;
                Debug.Log("The crate is moving left");
            }
            else if (_dir == direction.right)
            {
                collision.rigidbody.velocity = new Vector2(1, 0) * speed;
                Debug.Log("The crate is moving right");
            }
            else if (_dir == direction.up)
            {
                collision.rigidbody.velocity = new Vector2(0, 1) * speed;
                Debug.Log("The crate is moving up");
            }
            else if (_dir == direction.down)
            {
                collision.rigidbody.velocity = new Vector2(0, -1) * speed;
                Debug.Log("The crate is moving down");
            }

        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crate")
        {
            collision.rigidbody.velocity = Vector2.zero;
            Debug.Log("Resetting the velocity of the crate");
        }

    }

    void ClockRock()
    {
        Vector2 cast = new Vector2(1,0);
        if (_dir == direction.left)
        {
            cast = new Vector2(-1, 0);
        }
        else if (_dir == direction.right)
        {
            cast = new Vector2(1, 0);
        }
        else if (_dir == direction.up)
        {
            cast = new Vector2(0, 1);
        }
        else if (_dir == direction.down)
        {
            cast = new Vector2(0, -1);
        }
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position,cast,.5f);
        RaycastHit2D hit = hits[1];
        if (hit.collider.CompareTag("Clock Rock"))
        {
            hit.collider.GetComponent<ChangeTime>().sendThemBack();
        }
    }
}

