using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ButtonManager : MonoBehaviour
{
    private Button start;
    //private Button quit; 

    // Start is called before the first frame update
    void Start()
    {
        AddButtonListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddButtonListeners()
    {
        start = GameObject.Find("Start Button").GetComponent<Button>();
        start.onClick.AddListener(StartGame);
        //quit = GameObject.Find("Quit Button").GetComponent<Button>();
        // quit.onClick.AddListener(QuitGame);
    }
    void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    /*void QuitGame()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }*/
}
