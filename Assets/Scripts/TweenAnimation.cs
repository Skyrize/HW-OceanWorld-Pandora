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
        animation.Restart();
    }
}

public abstract class TweenAnimation : ScriptableObject
{
    [Header("Settings (Tween Animation):")]
    [SerializeField] protected string _name = "default name";
    [SerializeField] protected float duration = 1f;
    [SerializeField] protected Ease generalEase = Ease.Linear;
    [SerializeField] protected bool reverse = true;
    [SerializeField] protected int loops = 0;
    [SerializeField] protected LoopType loopType = LoopType.Yoyo;
    public string Name {
        get {
            return _name;
        }
    }

    public abstract GeneratedAnimation GenerateAnimation(Transform target);
}