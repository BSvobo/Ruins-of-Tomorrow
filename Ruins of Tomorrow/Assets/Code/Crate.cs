using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public Rigidbody2D _rb;
    private AudioSource audioSource;
    public AudioClip DraggingSound;
    public static bool BeingMoved = false;
    private bool OnFire = false;
    private SpriteRenderer sprite;
    private Transform _transform;
    private Color burnt;
    public GameObject FireAnim;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        burnt = new Color(.9f, .9f, .9f, 1f);
        _transform = GetComponent<Transform>();
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

        if (sprite.color.r < .32f) //change back to .3f
        {
            if (!OnFire)
            {
                OnFire = true;
                Instantiate(FireAnim, _transform.position, Quaternion.identity);
                Debug.Log("Instatiating fire animation");
            }

        }
        if (sprite.color.r < .3f)
        {
            destroy();
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

        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                BeingMoved = true;

                Vector2 new_velocity = col.attachedRigidbody.velocity;
                _rb.velocity = new_velocity;
                col.attachedRigidbody.velocity = new_velocity;
            }
            else if (Input.GetKey(KeyCode.Space) == false)
            {
                BeingMoved = false;
            }
            col.gameObject.GetComponent<Player>().pushPull();
        }
    }

    private void destroy()
    {
        Destroy(gameObject);
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
