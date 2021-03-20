using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public PauseManager pauseManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterInMenu()
    {
        pauseManager.Pause();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }

    public void ExitMenu()
    {
        if (pauseManager.paused)
        {
            pauseManager.Unpause();
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("MainMenu");
        }
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
