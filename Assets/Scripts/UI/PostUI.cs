using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected Color focusColor = Color.yellow;
    [Header("Events")]
    [SerializeField] public CrewMemberEvent onSelect = new CrewMemberEvent();
    [SerializeField] public PostEvent onFree = new PostEvent();
    [SerializeField] public PostEvent onDrop = new PostEvent();
    [Header("References")]
    [SerializeField] protected SelectCrewMemberButtonUI button = null;
    [SerializeField] protected Image postIcon = null;
    [SerializeField] protected StatusBarUI statusBar = null;
    [SerializeField] protected TMPro.TMP_Text nameText = null;
    [Header("Runtime")]
    [SerializeField] protected Post post = null;
    [SerializeField] public Post CurrentPost => post;

    Color[] tmpColors;
    Transform meshTransform = null;
    private void Start() {
        if (CurrentPost) {
            meshTransform = CurrentPost.transform.Find("Mesh");
            tmpColors = CurrentPost.originPrefab.transform.Find("Mesh").gameObject.GetSharedColors();
        }
        button.onSelect.AddListener(this.Select);
        button.onUnselect.AddListener(this.Free);
        button.onDrop.AddListener(this.Drop);
    }

    public void UpdateUI(Post post)
    {
        this.post = post;
        // meshTransform = CurrentPost.transform.Find("Mesh");
        // tmpColors = meshTransform.gameObject.GetColors();
        Item postItem = post.GetComponent<ItemObject>().Item;
        statusBar.UpdateUI(post.Employee);
        button.UpdateUI(post.Employee);
        nameText.text = postItem.Name;
        postIcon.sprite = postItem.Icon;
    }

    public void Select(CrewMember crewMember)
    {
        this.onSelect.Invoke(crewMember);
    }

    public void Free()
    {
        onFree.Invoke(post);
    }
    public void Drop()
    {
        onDrop.Invoke(post);
    }


    public void FocusPost()
    {
        meshTransform.gameObject.SetColor(focusColor);
    }

    public void UnfocusPost()
    {
        meshTransform.gameObject.SetColors(tmpColors);
    }

    private void OnDisable() {
        meshTransform.gameObject.SetColors(tmpColors);
        
    }

    private void OnDestroy() {
        meshTransform.gameObject.SetColors(tmpColors);
    }
}
