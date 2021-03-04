using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "TweenAnimation/RotationAnimation")]
public class RotationAnimation : TweenAnimation
{
    [Header("Settings (Rotation Animation):")]
    [SerializeField] protected Vector3 rotation = Vector3.one;

    override public GeneratedAnimation GenerateAnimation(Transform target) {
        Sequence animation = DOTween.Sequence();
        if (reverse) {
            animation.Append(target.DORotate(rotation, duration / 2).SetEase(generalEase));
            animation.Append(target.DORotate(target.localEulerAngles, duration / 2).SetEase(generalEase));
        } else {
            animation.Append(target.DORotate(rotation, duration));
        }
        animation.SetAutoKill(false);
        animation.SetLoops(loops, loopType);
        // animation.SetEase(generalEase);
        animation.Pause();
        return new GeneratedAnimation(animation, Name);
    }

}
