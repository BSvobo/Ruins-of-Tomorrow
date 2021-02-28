using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    public Image black;
    public Animator anim;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public Text level_name;

    // Start is called before the first frame update
    void Start()
    {
        level_name.text = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("Fade");
            audioSource.PlayOneShot(audioClip, .9f);
        }
    }

    public void FadingComplete()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (SceneManager.sceneCountInBuildSettings == activeScene.buildIndex + 1)
        {
            Debug.Log("Congratulations! You delved all the way into the ruins!");
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(activeScene.buildIndex + 1);
            Debug.Log("Level " + (activeScene.buildIndex) + " Completed Succesfully: Loading Next Level");
        }
    }
}
