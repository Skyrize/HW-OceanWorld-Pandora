using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Manager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{

    public void ReloadCurrentScene()
    {
        Manager.LoadScene(Manager.GetActiveScene().name);
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = Manager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }
    public void LoadScene(string sceneName)
    {
        Manager.LoadScene(sceneName);
    }

    public void LoadSceneAsyncro(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public void Exit()
    {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
    }
}
