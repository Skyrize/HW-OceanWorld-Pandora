using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Should get ridden of
/// </summary>
public class TestEnemyShoot : MonoBehaviour
{
    // private float blinkDuration;

    // private float lastBlink = 0;
    // private float blinkInterval = .2f;

    // private MeshRenderer sophisticatedBlinkEngineV2;

    [SerializeField] protected Weaponry weapons = null;
    public GameObject player;

    void Start()
    {
        if (!weapons)
            weapons = GetComponent<Weaponry>();
        // sophisticatedBlinkEngineV2 = GetComponent<MeshRenderer>();

        player = GameObject.FindGameObjectWithTag(Utils.Tags.PLAYER);

        InputManager.Instance.AddKeyEvent(KeyCode.Space, PressType.DOWN, Shoot);
    }

    public void Shoot()
    {
        weapons.ShootAt(player.transform.position);

        // if (!IsBlinking) { sophisticatedBlinkEngineV2.enabled = true; return; }

        // blinkDuration -= Time.deltaTime;
        // lastBlink += Time.deltaTime;

        // if (lastBlink < blinkInterval)
        //     return;
           
        // sophisticatedBlinkEngineV2.enabled = !sophisticatedBlinkEngineV2.enabled;
        // lastBlink = 0;
    }

    // private bool IsBlinking => blinkDuration > 0;

    // public void GetHit()
    // {
    //     // print($"I, ennemy, was hit by a { p.Type } !");
    //     blinkDuration = 2f;
    // }
}
