using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesTransition : MonoBehaviour
{
    public HealthComponent healthPlayer;
    private bool isLoaded;

    // Update is called once per frame
    void Update()
    {
        if(healthPlayer.Health <= 0 && !isLoaded)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene", LoadSceneMode.Additive);
            isLoaded = true;
        }            
    }
}
