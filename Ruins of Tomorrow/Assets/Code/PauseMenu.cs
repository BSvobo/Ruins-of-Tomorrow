using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public Object _pauseMenu;
    public static Transform Canvas;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        Canvas = GameObject.Find("Canvas").transform;
        InitializeButtons();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToGame();
        }
    }

    //Initializes the buttons of the pause menu
    private void InitializeButtons()
    {
        Button returnButton = GameObject.Find("Return").GetComponent<Button>();
        returnButton.onClick.AddListener(() => ReturnToGame());

        Button quitButton = GameObject.Find("PauseQuit").GetComponent<Button>();
        quitButton.onClick.AddListener(() => QuitGame());

        Button startMenuButton = GameObject.Find("StartMenu").GetComponent<Button>();
        startMenuButton.onClick.AddListener(() => GoToStartMenu());

        Button restartLevelButton = GameObject.Find("Restart Level").GetComponent<Button>();
        restartLevelButton.onClick.AddListener(() => Restart());
    }

    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ReturnToGame()
    {
        Time.timeScale = 1f;
        GameObject pauseMenu = GameObject.Find("Pause Menu");
        Player.inPauseMenu = false;
        Destroy(gameObject);
        GameObject.Find("Player").GetComponent<Player>().active = true;
    }

    public void GoToStartMenu()
    {
        SceneManager.LoadScene("Start menu");
        ReturnToGame();
        Debug.Log("Destroying Pause Menu");

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ReturnToGame();
    }
}
