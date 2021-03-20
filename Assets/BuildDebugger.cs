using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildDebugger : MonoBehaviour
{
    TMPro.TMP_Text text = null;
    Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindObjectOfType<Player>().inventory;
        text = GetComponent<TMPro.TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = inventory.ToString();
    }
}
