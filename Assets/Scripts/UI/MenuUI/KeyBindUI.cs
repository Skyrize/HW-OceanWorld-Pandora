using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected TMPro.TMP_Text keyText = null;
    [SerializeField] protected TMPro.TMP_Text descriptionText = null;

    public void UpdateUI(KeyBind item)
    {
        keyText.text = item.key;
        descriptionText.text = item.description;
    }
}
