using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Image image;

    Color color;
    // Start is called before the first frame update
    void Start()
    {
        if (!image) {
            image = GetComponent<Image>();
        }
        color = image.color;
    }

    public void UpdateRatio(float ratio)
    {
        if (!image) {
            image = GetComponent<Image>();
        }
        if (ratio <= 0.33f) {
            image.color = Color.red;
        } else {
            image.color = color;
        }
        image.fillAmount = ratio;
    }

}