using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyManager : MonoBehaviour, IHitable
{
    private float blinkDuration;

    private float lastBlink = 0;
    private float blinkInterval = .2f;

    private MeshRenderer sophisticatedBlinkEngineV2;

    public WeaponManager canon;
    public GameObject player;

    void Start()
    {
        sophisticatedBlinkEngineV2 = GetComponent<MeshRenderer>();
        canon.TargetType = Utils.Tags.PLAYER;

        player = Utils.FindGameObject(Utils.Tags.PLAYER);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            canon.ShootAt(player.transform.position);

        if (!IsBlinking) { sophisticatedBlinkEngineV2.enabled = true; return; }

        blinkDuration -= Time.deltaTime;
        lastBlink += Time.deltaTime;

        if (lastBlink < blinkInterval)
            return;
           
        sophisticatedBlinkEngineV2.enabled = !sophisticatedBlinkEngineV2.enabled;
        lastBlink = 0;
    }

    private bool IsBlinking => blinkDuration > 0;

    public void HitBy(Projectile p)
    {
        print($"I, ennemy, was hit by a { p.Type } !");
        blinkDuration = 2f;
    }
}
