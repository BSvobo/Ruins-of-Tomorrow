using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeepPit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
                collision.gameObject.GetComponent<Animator>().SetBool("dying", true);
                collision.gameObject.GetComponent<Player>().playFallingSound();
                //StartCoroutine("ResetLevelCo");
            }
        }
        else if (collision.gameObject.CompareTag("Crate") && this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "pitSpriteWithBridge")
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
    }
    public IEnumerator ResetLevelCo()
    {
        yield return new WaitForSeconds(.694f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
