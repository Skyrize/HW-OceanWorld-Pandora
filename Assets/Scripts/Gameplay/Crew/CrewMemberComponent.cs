using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrewMemberComponent : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected CrewMember crewMemberAsset;
    public CrewMember crewMember = null;
    // Start is called before the first frame update
    void Start()
    {
        crewMember = Instantiate(crewMemberAsset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
