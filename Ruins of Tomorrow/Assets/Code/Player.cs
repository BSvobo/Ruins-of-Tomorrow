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
    private float normalSpeed;
    private Timeable _timeable;

    public static bool inPauseMenu = false;
    public Object _pauseMenu;
    public static Transform Canvas;

    public enum direction {left, right, up, down};
    public direction _dir;
    private SpriteRenderer sprite;
    private Animator animator;

    private bool isWalking = false;
    //public AudioClip WalkingSound;
    private AudioSource audioSource;

    public AudioClip falling;
    
    
    //private bool moving;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        _rb.velocity = new Vector2(0, 0);
        _dir = direction.right;
        sprite.flipX = false;
        Canvas = GameObject.Find("Canvas").transform;
        normalSpeed = speed;
       
        //animator.SetBool("walking", false);
        

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
        if (_rb.velocity != Vector2.zero)
        {
            isWalking = true;
        }

        if (isWalking == true)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
        
        //Make speed slower when pushing/pulling crate
        if (Crate.BeingMoved)
        {
            speed = normalSpeed * .5f;
        }
        else
        {
            speed = normalSpeed;
        }

    }

    void HandleInputs()
    {
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            _rb.velocity = Vector2.left *speed;
            _dir = direction.left;
            sprite.flipX = true;
            animator.SetBool("walking",true);
        }
        else if (Input.GetKey("right") || Input.GetKey("d"))
        {
            _rb.velocity = Vector2.right *speed;
            _dir = direction.right;
            sprite.flipX = false;
            animator.SetBool("walking",true);
        }
        else if (Input.GetKey("up") || Input.GetKey("w"))
        {
            _rb.velocity = Vector2.up *speed;
            _dir = direction.up;
            animator.SetBool("walking",true);
        }
        else if (Input.GetKey("down") || Input.GetKey("s"))
        {
            _rb.velocity = Vector2.down *speed;
            _dir = direction.down;
            animator.SetBool("walking", true);
        }
        else
        {
            _rb.velocity = Vector2.zero;
            isWalking = false;
            animator.SetBool("walking", false);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crate" && Input.GetKey(KeyCode.E))
        {
            //animator.SetBool("pushing", true);
            if (_dir == direction.left)
            {
                collision.rigidbody.velocity = new Vector2(-1, 0) * speed;
            }
            else if (_dir == direction.right)
            {
                collision.rigidbody.velocity = new Vector2(1, 0) * speed;
            }
            else if (_dir == direction.up)
            {
                collision.rigidbody.velocity = new Vector2(0, 1) * speed;
            }
            else if (_dir == direction.down)
            {
                collision.rigidbody.velocity = new Vector2(0, -1) * speed;
            }

        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crate" && Input.GetKey(KeyCode.Space))
        {

            //animator.SetBool("pushing", true);
            if (_dir == direction.left)
            {
                collision.rigidbody.velocity = new Vector2(-1, 0) * speed * .5f;
            }
            else if (_dir == direction.right)
            {
                collision.rigidbody.velocity = new Vector2(1, 0) * speed * .5f;
            }
            else if (_dir == direction.up)
            {
                collision.rigidbody.velocity = new Vector2(0, 1) * speed * .5f;
            }
            else if (_dir == direction.down)
            {
                collision.rigidbody.velocity = new Vector2(0, -1) * speed * .5f;
            }
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

    /*public void playFallingSound()
    {
        audioSource.PlayOneShot(falling);
    }*/
}

