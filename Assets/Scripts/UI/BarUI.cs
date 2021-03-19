using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Image image;
    [SerializeField] protected Image colorImage;

    Color color;
    // Start is called before the first frame update
    void Start()
    {
        if (!image) {
            image = GetComponent<Image>();
        }
        color = colorImage.color;
    }

    public void UpdateRatio(float ratio)
    {
        if (!image) {
            image = GetComponent<Image>();
        }
        if (ratio <= 0.33f) {
            colorImage.color = Color.red;
        } else if (ratio <= 0.5f) {
            colorImage.color = Color.yellow;
        } else {
            colorImage.color = color;
        }
        image.fillAmount = ratio;
    }

}