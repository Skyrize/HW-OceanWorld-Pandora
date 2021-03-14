using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostManagerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected CrewUI crewUI = null;
    [SerializeField] protected Player player = null;
    [SerializeField] protected RectTransform PostPanelContent = null;
    [SerializeField] protected GameObject PostCardPrefab = null;
    [SerializeField] List<PostUI> postCards = new List<PostUI>();

    public void ClickPost(Post post)
    {
        PostUI target = postCards.Find((postUI) => postUI.CurrentPost == post);
        Debug.Log("Place");
        if (!crewUI.CurrentCrewMember)
            return;
        if (!post.Employee || post.Employee != crewUI.CurrentCrewMember) {
        Debug.Log("Employ");
            post.SetEmployee(crewUI.CurrentCrewMember);
        }
        Debug.Log("Update");
        target.UpdateUI(post);
    }

    public void ClearPost(Post post)
    {
        PostUI target = postCards.Find((postUI) => postUI.CurrentPost == post);
        post.ClearEmployee();
        target.UpdateUI(post);
    }

    public void RemovePost(Post post)
    {
        PostUI target = postCards.Find((postUI) => postUI.CurrentPost == post);

        postCards.Remove(target);
        GameObject.Destroy(target.gameObject);
    }

    public void AddPost(Post post)
    {
        PostUI cardUI = GameObject.Instantiate(PostCardPrefab, PostPanelContent).GetComponent<PostUI>();

        cardUI.UpdateUI(post);
        cardUI.onSelect.AddListener(this.ClickPost);
        cardUI.onFree.AddListener(this.ClearPost);
        postCards.Add(cardUI);
    }

    public void ClearUI()
    {
        PostPanelContent.ClearChilds(); // TODO: easy but dirty. Maybe remove them along when adding to inventory
    }
    
    public void BuildUI()
    {
        AddPost(player.controlPost);
        AddPost(player.repairStation);
        foreach (Post post in player.weaponry.weapons)
        {
            AddPost(post);
        }
    }

    private void OnEnable() {
        ClearUI();
        BuildUI();
    }

    private void OnDisable() {
        ClearUI();
    }
}
