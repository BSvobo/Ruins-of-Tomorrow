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
    public Light clock_rock_halo;
    
    
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
        //if (Input.GetKey("left") || Input.GetKey("a"))
        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            _rb.velocity = Vector2.left *speed;
            _dir = direction.left;
            animator.SetBool("walkingL",true);
            animator.SetBool("walkingR", false);

        }
        else if (Input.GetKey("right") || Input.GetKey("d"))
        {
            _rb.velocity = Vector2.right *speed;
            _dir = direction.right;
            animator.SetBool("walkingR",true);
            animator.SetBool("walkingL", false);

        }
        else if (Input.GetKey("up") || Input.GetKey("w"))
        {
            _rb.velocity = Vector2.up *speed;
            _dir = direction.up;
            animator.SetBool("walkingR",true);
            animator.SetBool("walkingL", false);

        }
        else if (Input.GetKey("down") || Input.GetKey("s"))
        {
            _rb.velocity = Vector2.down *speed;
            _dir = direction.down;
            animator.SetBool("walkingL", true);
            animator.SetBool("walkingR", false);

        }
        else
        {
            _rb.velocity = Vector2.zero;
            isWalking = false;
            animator.SetBool("walkingR", false);
            animator.SetBool("walkingL", false);

        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO: Do we still need this?
        if (collision.gameObject.tag == "Crate" && Input.GetKey(KeyCode.E))
        {
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
        /*else if (collision.gameObject.tag == "Clock Rock")
        {
            clock_rock_halo = collision.gameObject.GetComponent<Light>();
            clock_rock_halo.enabled = true;
        }*/
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crate" && Input.GetKey(KeyCode.Space))
        {
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

    /*void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Clock Rock"))
        {
            clock_rock_halo = collision.gameObject.GetComponent<Light>();
            clock_rock_halo.enabled = false;
        }
    }*/


    void ClockRock()
    {
        //TODO: check the google doc
        var cast = new Vector2(1,0);
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

    public void pushPull()
    {
        if (Input.GetKey("space"))
        {
            var origin = transform.position;
            RaycastHit2D[] hitsU = Physics2D.RaycastAll(origin,Vector2.up,.5f);
            RaycastHit2D[] hitsD = Physics2D.RaycastAll(origin,Vector2.down,.5f);
            RaycastHit2D[] hitsL = Physics2D.RaycastAll(origin,Vector2.left,.5f);
            RaycastHit2D[] hitsR = Physics2D.RaycastAll(origin,Vector2.right,.5f);

            if (hitsU.Length > 1 && hitsU[1].collider.gameObject.CompareTag("Crate"))
            {
                animator.SetBool("pushing",true);
                animator.SetBool("up",true);
            }
            else if (hitsD.Length > 1 && hitsD[1].collider.gameObject.CompareTag("Crate"))
            {
                animator.SetBool("pushing",true);
                animator.SetBool("down",true);
            }
            else if (hitsL.Length > 1 && hitsL[1].collider.gameObject.CompareTag("Crate"))
            {
                animator.SetBool("pushing",true);
                sprite.flipX = true;
            }
            else if (hitsR.Length > 1 && hitsR[1].collider.gameObject.CompareTag("Crate"))
            {
                animator.SetBool("pushing",true);
                sprite.flipX = false;
            } else {
                sprite.flipX = false;
                animator.SetBool("up",false);
                animator.SetBool("down",false);
                animator.SetBool("pushing",false);
            }
        }
        else
        {
            sprite.flipX = false;
            animator.SetBool("up",false);
            animator.SetBool("down",false);
            animator.SetBool("pushing",false);
        }
    }
}

