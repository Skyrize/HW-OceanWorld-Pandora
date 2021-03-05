using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrewManager : MonoBehaviour
{
    [Header("Runtime")]
    [SerializeField] protected bool toggle = false;
    // Start is called before the first frame update
    protected EventBinder events;
    void Start()
    {
        events = GetComponent<EventBinder>();
    }

    public void Toggle()
    {
        toggle = !toggle;
        if (toggle) {
            events.CallEvent("Enter");
        } else {
            events.CallEvent("Exit");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
