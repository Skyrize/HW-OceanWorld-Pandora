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
        if (UnityEngine.SceneManagement.SceneManager.GetSceneByName("Level 1 copy").isLoaded)
        {      
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("MainMenu");
            PauseManager pause = (PauseManager)GameObject.Find("Pause Manager").GetComponent(typeof(PauseManager));
            pause.Unpause();
        }
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1 copy");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
