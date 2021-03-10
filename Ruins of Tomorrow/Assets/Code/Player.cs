using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

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

    public enum direction {left, right, up, down,still};
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

    public bool pushing;
    public bool active = false;
    
    
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
        pushing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            SetDirection();
            Move();
            Animate();

            Menu();

            WalkingSound();


            //Make speed slower when pushing/pulling crate
            if (pushing)
            {
               
            }
            else
            {
                
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
    }

    void SetDirection()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            _dir = direction.left;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            _dir = direction.right;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            _dir = direction.down;
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            _dir = direction.up;
        }
        else
        {
            _dir = direction.still;
        }
    }

    void Move()
    {
        if (pushing)
        {
            speed = normalSpeed * .5f;
        }
        else if (animator.GetBool("dying") || animator.GetBool("falling"))
        {
            speed = 0;
        }
        else
        {
            speed = normalSpeed;
            
        }

        if (_dir == direction.left)
        {
            _rb.velocity = Vector2.left * speed;
        }
        else if (_dir == direction.right)
        {
            _rb.velocity = Vector2.right * speed;
        }
        else if (_dir == direction.down)
        {
            _rb.velocity = Vector2.down * speed;
        }
        else if (_dir == direction.up)
        {
            _rb.velocity = Vector2.up * speed;
        }
        else if (_dir == direction.still)
        {
            _rb.velocity = Vector2.zero;
        }
    }

    void Animate()
    {
        if (pushing)
        {
            animator.SetBool("walkingR", false);
            animator.SetBool("walkingL", false);
            pushPull();
            
        }
        else
        {
            animator.SetBool("pushing", false);
            animator.SetBool("up", false);
            animator.SetBool("down", false);
            sprite.flipX = false;

            if (_dir == direction.left || _dir == direction.down)
            {
                animator.SetBool("walkingL", true);
                animator.SetBool("walkingR", false);
            }
            else if (_dir == direction.right || _dir == direction.up)
            {
                animator.SetBool("walkingR", true);
                animator.SetBool("walkingL", false);
            }
            else
            {
                animator.SetBool("walkingR", false);
                animator.SetBool("walkingL", false);
            }
        }
    }

    void Menu()
    {
        //Instatiate or destroy Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inPauseMenu == false)
            {
                inPauseMenu = true;
                active = false;
                Instantiate(_pauseMenu, Canvas);
            }
            else
            {
                //Return to Game
                active = true;
            }
        }
    }

    void WalkingSound()
    {
        if (_rb.velocity != Vector2.zero)
        {
            isWalking = true;
        }else{
            isWalking = false;
        }

        if (isWalking)
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
        if (inPauseMenu)
        {
            audioSource.Stop();
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

    private void pushPull()
    {
        animator.SetBool("pushing", true);    
        var origin = transform.position;
        RaycastHit2D[] hitsU = Physics2D.RaycastAll(origin,Vector2.up,.5f); 
        RaycastHit2D[] hitsD = Physics2D.RaycastAll(origin,Vector2.down,.5f);
        RaycastHit2D[] hitsL = Physics2D.RaycastAll(origin,Vector2.left,.5f);
        RaycastHit2D[] hitsR = Physics2D.RaycastAll(origin,Vector2.right,.5f);

        if (FindCrate(hitsU))
        {
            animator.SetBool("up",true);
        }
        else if (FindCrate(hitsD))
        {
            animator.SetBool("down",true);
        }
        else if (FindCrate(hitsL))
        {
            sprite.flipX = true;
        }
        else if (FindCrate(hitsR))
        {
            sprite.flipX = false;
        }
    }

    private bool FindCrate(RaycastHit2D[] hitsList)
    {
        for (int i = 0; i < hitsList.Length; i++)
        {
            if (hitsList[i].collider.gameObject.CompareTag("Crate") || hitsList[i].collider.gameObject.CompareTag("Metal Box"))
            {
                return true;
            }
        }
        return false;
    }
}

