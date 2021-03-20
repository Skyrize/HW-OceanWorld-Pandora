using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomHealthTextUI : MonoBehaviour
{
    [SerializeField] HealthComponent health;
    [SerializeField] TMPro.TMP_Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = $"{(int)health.Health} / {(int)health.MaxHealth}";
    }
}
