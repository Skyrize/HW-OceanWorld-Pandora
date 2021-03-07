using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyManager : MonoBehaviour, IHitable
{
    private float blinkDuration;

    private float lastBlink = 0;
    private float blinkInterval = .2f;

    private MeshRenderer sophisticatedBlinkEngineV2;

    void Start()
    {
        sophisticatedBlinkEngineV2 = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (!IsBlinking) { sophisticatedBlinkEngineV2.enabled = true; return; }

        blinkDuration -= Time.deltaTime;
        lastBlink += Time.deltaTime;

        if (lastBlink < blinkInterval)
            return;
           
        sophisticatedBlinkEngineV2.enabled = !sophisticatedBlinkEngineV2.enabled;
        lastBlink = 0;
    }

    private bool IsBlinking => blinkDuration > 0;

    public void hitBy(Projectile p)
    {
        print($"I was hit by a { p.Type }!");
        blinkDuration = 2f;
    }
}
