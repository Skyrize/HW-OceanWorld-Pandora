using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "TweenAnimation/ImageAlphaAnimation")]
public class ImageAlphaAnimation : TweenAnimation
{
    [Header("Settings")]
    [SerializeField] protected Color endColor = Color.black;

    override public GeneratedAnimation GenerateAnimation(Transform target) {
        Sequence animation = DOTween.Sequence();
        Image image = target.GetComponent<Image>();

        if (!image)
            throw new System.Exception("Can't generate ImageAlphaAnimation without an Image on the target transform");
        if (reverse) {
            animation.Append(image.DOColor(endColor, duration / 2).SetEase(generalEase));
            animation.Append(image.DOColor(image.color, duration / 2).SetEase(generalEase));
        } else {
            animation.Append(image.DOColor(endColor, duration).SetEase(generalEase));
        }
        animation.SetAutoKill(false);
        animation.SetLoops(loops, loopType);
        // animation.SetEase(generalEase);
        animation.Pause();
        return new GeneratedAnimation(animation, Name);
    }
}
