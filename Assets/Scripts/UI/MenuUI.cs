using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public PauseManager pauseManager;
    public static bool isInMenu;

    public void EnterInMenu()
    {
        if (!isInMenu)
        {
            pauseManager.Pause();
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
            isInMenu = true;
        }
    }

    public void ExitMenu()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetSceneByName("Level 1 copy").isLoaded)
        {      
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("MainMenu");
            PauseManager pause = (PauseManager)GameObject.Find("Pause Manager").GetComponent(typeof(PauseManager));
            pause.Unpause();
        }
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1 copy");
        isInMenu = false;

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
