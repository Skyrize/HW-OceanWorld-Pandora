using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    
    public List<string> listOfLevelScenes = null;

    // Start is called before the first frame update
    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        foreach(string scene in listOfLevelScenes)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetSceneByName(scene).isLoaded)
            {
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("GameOverScene");
                UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
            }

        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("WinScene");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");
    }
}
