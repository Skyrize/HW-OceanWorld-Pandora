using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Flock : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected List<FlockAgent> agents = new List<FlockAgent>();
    [SerializeField] protected FlockBehavior behavior;



    private void Awake() {
    }
    // Start is called before the first frame update
    void Start()
    {
        
        foreach (FlockAgent agent in agents)
        {
            agent.flock = this;
        }
    }

    public abstract void UpdateAgent(FlockAgent agent);

    virtual public void UpdateAgents()
    {
        foreach (FlockAgent agent in agents)
        {
            if (agent && agent.isActiveAndEnabled) {

                UpdateAgent(agent);
            }
        }

    }

}
