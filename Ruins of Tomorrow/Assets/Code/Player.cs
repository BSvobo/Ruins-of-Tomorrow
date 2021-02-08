using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rb;
    public float speed;
    
    enum direction {left, right, up, down};
    private direction _dir;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = new Vector2(0, 0);
        _dir = direction.right;
    }

    // Update is called once per frame
    void Update()
    {
       HandleInputs();
    }

    void HandleInputs()
    {
        if (Input.GetKeyDown("left"))
        {
            _dir = direction.left;
            print("left");
        }
        else if (Input.GetKeyDown("right"))
        {
            _dir = direction.right;
            print("right");
        }
        else if (Input.GetKeyDown("up"))
        {
            _dir = direction.up;
            print("up");
        }
        else if (Input.GetKeyDown("down"))
        {
            _dir = direction.down;
            print("down");
        }
        else if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical")!=0)
        {
            Move();
        }
        else
        {
            _rb.velocity = new Vector2(0,0);
        }
    }

    void Move()
    {
        if (_dir == direction.left)
        {
            _rb.velocity = new Vector2(-1,0) *speed;
        }
        else if (_dir == direction.right)
        {
            _rb.velocity = new Vector2(1,0) *speed;
        }
        else if (_dir == direction.up)
        {
            _rb.velocity = new Vector2(0,1) *speed;
        }
        else if (_dir == direction.down)
        {
            _rb.velocity = new Vector2(0, -1) *speed;
        }
    }
    /*void HandleInputs()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown("left"))
            {
                 _rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0) * speed;
                 isMoving = true;
            } 
            else if (Input.GetKeyDown("right"))
            {
                _rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0) * speed;
                isMoving = true;
            }
            else if (Input.GetKeyDown("up"))
            {
                _rb.velocity = new Vector2(0, Input.GetAxisRaw("Vertical")) * speed;
                isMoving = true; 
            } 
            else if(Input.GetKeyDown("down"))
            {
                _rb.velocity = new Vector2(0, Input.GetAxisRaw("Vertical")) * speed;
                isMoving = true;
            }
        }
        else
        {
            if (Input.GetKeyUp("left") || Input.GetKeyUp("right"))
            {
                _rb.velocity = new Vector2(0,_rb.velocity.y);
                isMoving = false;
            } 
            else if(Input.GetKeyUp("up") || Input.GetKeyUp("down"))
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
                isMoving = false;
            }
        }
    }*/
}

