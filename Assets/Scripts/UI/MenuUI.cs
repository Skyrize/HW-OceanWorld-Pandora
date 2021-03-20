using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public void ExitMenu()
    {
        pauseManager.Unpause();
    }
}
