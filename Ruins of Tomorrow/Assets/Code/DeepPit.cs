using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeepPit : MonoBehaviour
{
    public AudioClip playerFell;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "pitSpriteWithBridge")
            {
                Debug.Log("Player entered collider of pit with bridge, ignoring");
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
                return;
            }
            else
            {
                
                Debug.Log("Player collided with pit, resetting");
                collision.gameObject.GetComponent<Animator>().SetBool("falling", true);
                audioSource.PlayOneShot(playerFell);
                StartCoroutine("ResetLevelCo");
            }
        }
        else if (collision.gameObject.CompareTag("Crate") && this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "pitSpriteWithBridge")
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
        else if (collision.gameObject.CompareTag("Metal Box") && this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "pitSpriteWithBridge")
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
    }
    public IEnumerator ResetLevelCo()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //, LoadSceneMode.Single);
    }
}
