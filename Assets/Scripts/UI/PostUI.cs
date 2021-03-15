using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected Color focusColor = Color.yellow;
    [Header("Events")]
    [SerializeField] public PostEvent onSelect = new PostEvent();
    [SerializeField] public PostEvent onFree = new PostEvent();
    [Header("References")]
    [SerializeField] protected SelectCrewMemberButtonUI button = null;
    [SerializeField] protected Image postIcon = null;
    [SerializeField] protected StatusBarUI statusBar = null;
    [SerializeField] protected TMPro.TMP_Text nameText = null;
    [Header("Runtime")]
    [SerializeField] protected Post post = null;
    [SerializeField] public Post CurrentPost => post;

    Color[] tmpColors;
    private void Start() {
        if (CurrentPost) {
            tmpColors = CurrentPost.gameObject.GetColors();
        }
        button.onSelect.AddListener(this.Select);
    }

    public void UpdateUI(Post post)
    {
        this.post = post;
        tmpColors = CurrentPost.gameObject.GetColors();
        Item postItem = post.GetComponent<ItemObject>().Item;
        statusBar.UpdateUI(post.Employee);
        button.UpdateUI(post.Employee);
        nameText.text = postItem.Name;
        postIcon.sprite = postItem.Icon;
    }

    public void Select(CrewMember crewMember)
    {
        this.onSelect.Invoke(post);
    }

    public void Free()
    {
        onFree.Invoke(post);
    }


    public void FocusPost()
    {
        CurrentPost.gameObject.SetColor(focusColor);
    }

    public void UnfocusPost()
    {
        CurrentPost.gameObject.SetColors(tmpColors);
        
    }

    private void OnDisable() {
        CurrentPost.gameObject.SetColors(tmpColors);
        
    }

    private void OnDestroy() {
        CurrentPost.gameObject.SetColors(tmpColors);
    }
}
