using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public Rigidbody2D _rb;
    private AudioSource audioSource;
    public AudioClip DraggingSound;
    private bool BeingPushed;
    private bool BeingMoved = false; 

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.angularVelocity = 0;

        if (!BeingMoved)
        {
            _rb.velocity = Vector2.zero;
            stopContinuous();
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                BeingMoved = true;
                _rb.velocity = collision.rigidbody.velocity;

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
            if (Input.GetKey(KeyCode.Space))
            {
                BeingMoved = true;
                /*if (col.gameObject.GetComponent<Player>()._dir == Player.direction.up)
                {
                    col.gameObject.GetComponent<Animator>().SetBool("up", true);
                }*/
                col.gameObject.GetComponent<Animator>().SetBool("pushing", true);


                Vector2 new_velocity = col.attachedRigidbody.velocity;
                _rb.velocity = new_velocity;
                col.attachedRigidbody.velocity = new_velocity;
            }
            else if (Input.GetKey(KeyCode.Space) == false)
            {
                BeingMoved = false;
                col.gameObject.GetComponent<Animator>().SetBool("pushing", false);

            }
        }
    }

    public void playSound(AudioClip sound)
    {
        if (sound == null)
            return;
        audioSource.loop = false;
        audioSource.PlayOneShot(sound);
    }

    public void playContinuous(AudioClip sound)
    {
        if (sound == null)
            return;
        audioSource.loop = false;
        audioSource.clip = sound;
        audioSource.Play();
    }

    public void stopContinuous()
    {
        audioSource.Stop();
    }

}
