using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager = UnityEngine.SceneManagement.SceneManager
;

[RequireComponent(typeof(AudioManager))]
public class MusicManager : MonoBehaviour
{
    private AudioManager audioManager = null;
    private bool inGame = false;

    private void Awake() {
        audioManager = GetComponent<AudioManager>();
    }

    private float timer = 0;
    private int musicIndex = 0;

    private void Update() {
        if (inGame) {

            if (timer <= 0) {
                audioManager.Play(musicIndex);
                timer = audioManager.GetClipLenght();
                musicIndex++;
            if (musicIndex == 4)
                musicIndex = 0;

            } else {
                timer -= Time.deltaTime;
            }
        }
    }

    void Start()
    {
        if (Manager.GetActiveScene().name == "Main Menu") {
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().clip = audioManager.GetClip("MenuMusic");
            GetComponent<AudioSource>().Play();
            inGame = false;
        } else {
            inGame = true;
            
        } 
    }

}
