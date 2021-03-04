using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "TweenAnimation/ScaleAnimation")]
public class ScaleAnimation : TweenAnimation
{
    [Header("Settings (Scale Animation):")]
    [SerializeField] protected Vector3 scale = Vector3.one;

    override public GeneratedAnimation GenerateAnimation(Transform target) {
        Sequence animation = DOTween.Sequence();
        if (reverse) {
            animation.Append(target.DOScale(scale, duration / 2));
            animation.Append(target.DOScale(target.localScale, duration / 2));
        } else {
            animation.Append(target.DOScale(scale, duration));
        }
        animation.SetAutoKill(false);
        animation.SetLoops(loops, loopType);
        animation.SetEase(generalEase);
        animation.Pause();
        return new GeneratedAnimation(animation, Name);
    }
}
