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

    public static bool inPauseMenu = false;
    public Object _pauseMenu;
    public static Transform Canvas;

    enum direction {left, right, up, down};
    private direction _dir;

    //private bool moving;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = new Vector2(0, 0);
        _dir = direction.right;
        Canvas = GameObject.Find("Canvas").transform;

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

        if (Input.GetKeyDown(KeyCode.Escape) && inPauseMenu == false)
        {
            inPauseMenu = true;
            Instantiate(_pauseMenu, Canvas);
        }
    }

    void HandleInputs()
    {
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            _rb.velocity = Vector2.left *speed;
            _dir = direction.left;
        }
        else if (Input.GetKey("right") || Input.GetKey("d"))
        {
            _rb.velocity = Vector2.right *speed;
            _dir = direction.right;
        }
        else if (Input.GetKey("up") || Input.GetKey("w"))
        {
            _rb.velocity = Vector2.up *speed;
            _dir = direction.up;
        }
        else if (Input.GetKey("down") || Input.GetKey("s"))
        {
            _rb.velocity = Vector2.down *speed;
            _dir = direction.down;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crate")
        {
            Debug.Log("The player collided with the crate");
            if (_dir == direction.left)
            {
                collision.rigidbody.velocity = new Vector2(-1, 0) * speed *.5f;
                Debug.Log("The crate is moving left");
            }
            else if (_dir == direction.right)
            {
                collision.rigidbody.velocity = new Vector2(1, 0) * speed *.5f;
                Debug.Log("The crate is moving right");
            }
            else if (_dir == direction.up)
            {
                collision.rigidbody.velocity = new Vector2(0, 1) * speed *.5f;
                Debug.Log("The crate is moving up");
            }
            else if (_dir == direction.down)
            {
                collision.rigidbody.velocity = new Vector2(0, -1) * speed * .5f;
                Debug.Log("The crate is moving down");
            }

        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crate")
        {
            if (Input.GetKey(KeyCode.E))
            {
                //collision.rigidbody.velocity = _rb.velocity;
            }
            else
            {
                collision.rigidbody.velocity = Vector2.zero;
                Debug.Log("Resetting the velocity of the crate");
            }
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crate" && Input.GetKey(KeyCode.E))
        {
            Debug.Log("Connecting the crate to the player");
            _rb.velocity = _rb.velocity * .5f;
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
        if (hits.Length > 1)
        {
            RaycastHit2D hit = hits[1];
            if (hit.collider.CompareTag("Clock Rock"))
            {
                hit.collider.GetComponent<ChangeTime>().sendThemBack();
            }
        }
    }
}

