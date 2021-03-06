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

    private GameObject clock_rock_to_be_changed;
    private bool clock_rock_toggleable;
    
    
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
        /*if (Input.GetKeyDown("space"))
        {
            ClockRock();
        }*/
        HandleInputs();

        //Instatiate or destroy Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inPauseMenu == false)
            {
                inPauseMenu = true;
                Instantiate(_pauseMenu, Canvas);
            }
            else
            {
                //Return to Game
                
            }
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

        //Toggle Clock Rock if we're in the range to do so
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (clock_rock_toggleable)
            {
                clock_rock_to_be_changed.GetComponent<ChangeTime>().sendThemBack();
            }
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


    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crate" || collision.gameObject.tag == "Metal Box")
        {
            if (Input.GetKey(KeyCode.Space))
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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Clock Rock"))
        {
            clock_rock_to_be_changed = other.gameObject;
            clock_rock_toggleable = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Clock Rock"))
        {
            clock_rock_toggleable = false;
            clock_rock_to_be_changed = null;
        }
    }

    private void destroy(Object thingToDestroy)
    {
        Destroy(thingToDestroy);
    }


    /*void ClockRock()
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
    }*/

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

