using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenAnimator : MonoBehaviour
{
    [Header("Animations")]
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private int awakeAnimationIndex = -1;
    [SerializeField]
    private TweenAnimation[] animations = null;
    [Header("Runtime")]
    private List<GeneratedAnimation> generatedAnimations = new List<GeneratedAnimation>();

    private void Awake() {
        if (!target)
            target = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (TweenAnimation animation in animations)
        {
            generatedAnimations.Add(animation.GenerateAnimation(target));
        }
        if (awakeAnimationIndex != -1) {
            Play(awakeAnimationIndex);
        }
    }

    public void Restart(int animationIndex)
    {
        if (animationIndex < 0 || animationIndex >= generatedAnimations.Count) {
            throw new System.Exception("Index out of bound");
        }
        generatedAnimations[animationIndex].Restart();
    }

    public void Restart(string animationName)
    {
        foreach (GeneratedAnimation anim in generatedAnimations)
        {
            if (anim.Name == animationName) {
                anim.Restart();
                return;
            }
        }
        Debug.LogWarning("Cant find animation named '" + animationName + "' !");
    }

    public void Restart(TweenAnimation animation)
    {
        foreach (GeneratedAnimation anim in generatedAnimations)
        {
            if (anim.Name == animation.name) {
                anim.Restart();
                return;
            }
        }
        Debug.LogWarning("Cant find animation named '" + animation.name + "' !");
    }
    
    public void Play(int animationIndex)
    {
        if (animationIndex < 0 || animationIndex >= generatedAnimations.Count) {
            throw new System.Exception("Index out of bound");
        }
        generatedAnimations[animationIndex].Play();
    }

    public void Play(string animationName)
    {
        foreach (GeneratedAnimation anim in generatedAnimations)
        {
            if (anim.Name == animationName) {
                anim.Play();
                return;
            }
        }
        Debug.LogWarning("Cant find animation named '" + animationName + "' !");
    }

    public void Play(TweenAnimation animation)
    {
        foreach (GeneratedAnimation anim in generatedAnimations)
        {
            if (anim.Name == animation.name) {
                anim.Play();
                return;
            }
        }
        Debug.LogWarning("Cant find animation named '" + animation.name + "' !");
    }
    
    public void Rewind(int animationIndex)
    {
        if (animationIndex < 0 || animationIndex >= generatedAnimations.Count) {
            throw new System.Exception("Index out of bound");
        }
        generatedAnimations[animationIndex].Rewind();
    }

    public void Rewind(string animationName)
    {
        foreach (GeneratedAnimation anim in generatedAnimations)
        {
            if (anim.Name == animationName) {
                anim.Rewind();
                return;
            }
        }
        Debug.LogWarning("Cant find animation named '" + animationName + "' !");
    }

    public void Rewind(TweenAnimation animation)
    {
        foreach (GeneratedAnimation anim in generatedAnimations)
        {
            if (anim.Name == animation.name) {
                anim.Rewind();
                return;
            }
        }
        Debug.LogWarning("Cant find animation named '" + animation.name + "' !");
    }
    
}
