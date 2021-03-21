using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public PauseManager pauseManager;
    public GameObject controlMenu;
    public GameObject goalMenu;
    public GameObject mainMenu;
    public static bool isInMenu;

    private void Awake()
    {
        if(goalMenu != null && controlMenu != null && mainMenu != null)
        {
            goalMenu.SetActive(false);
            controlMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
    
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

    public void DisplayControls()
    {
        controlMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void DisplayGoal()
    {
        goalMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void MainMenu()
    {
        controlMenu.SetActive(false);
        goalMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
