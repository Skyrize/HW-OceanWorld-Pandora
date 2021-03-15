using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CrewMemberEvent : UnityEvent<CrewMember>
{
}


[CreateAssetMenu(menuName = "CrewMember/CrewMember")]
public class CrewMember : ClonableSO
{
    [Header("Settings")]
    [SerializeField] protected string _name = "default Name";
    public string Name => _name;
    public List<Skill> skills = new List<Skill>();
    [Header("References")]
    public GameObject prefab;
    public Sprite icon;
    [Header("Runtime")]
    public Post currentPost;
    protected string status = "none";
    public string Status => status;
    protected string nextStatus = "";
    protected float hireTime = 1;
    protected float timer = 0;
    public float HireRatio {
        get {
            if (hireTime == 0)
                return 1;
            return timer / hireTime;
        }
    }

    public void Hire(Post post, string postName, float hireTime)
    {
        this.currentPost = post;
        nextStatus = postName;
        status = "Switching post..";
        this.hireTime = hireTime;
        timer = 0;
    }

    public void Update()
    {
        if (nextStatus != "") {
            if (timer >= hireTime) {
                status = nextStatus;
                nextStatus = "";
            } else {
                timer += Time.deltaTime;
            }
        }
    }

    public void Fire()
    {
        this.currentPost = null;
        status = "Free";
        nextStatus = "";
    }
}
