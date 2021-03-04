using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIHealthBar : MonoBehaviour
{
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        if (!image) {
            image = GetComponent<Image>();
        }
    }

    public void UpdateHealth(float ratio)
    {
        if (!image) {
            image = GetComponent<Image>();
        }
        if (ratio <= 0.33f) {
            image.color = Color.red;
        }
        image.fillAmount = ratio;
    }

}