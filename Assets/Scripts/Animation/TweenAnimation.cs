using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class GeneratedAnimation
{
    public GeneratedAnimation(Sequence sequence, string name) {
        this.animation = sequence;
        this.name = name;
    }

    [SerializeField] protected Sequence animation = null;
    [SerializeField] protected string name = "";
        public string Name {
        get {
            return name;
        }
    }

    public void Play()
    {
        animation.PlayForward();
    }

    public void Restart()
    {
        animation.Restart();
    }

    public void Rewind()
    {
        animation.PlayBackwards();
    }
}

public abstract class TweenAnimation : ScriptableObject
{
    [Header("Settings (Tween Animation):")]
    [SerializeField] protected float duration = 1f;
    [SerializeField] protected Ease generalEase = Ease.Linear;
    [SerializeField] protected bool reverse = true;
    [SerializeField] protected bool playWhenPause = true;
    [SerializeField] protected int loops = 0;
    [SerializeField] protected LoopType loopType = LoopType.Yoyo;

    public abstract GeneratedAnimation GenerateAnimation(Transform target);

}