using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
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
        if (collision.gameObject.tag == "Player")
        {
            Scene activeScene = SceneManager.GetActiveScene();
            if (SceneManager.sceneCountInBuildSettings == activeScene.buildIndex+1)
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
}
