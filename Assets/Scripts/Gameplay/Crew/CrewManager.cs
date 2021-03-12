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
        InputManager.Instance.AddKeyEvent(KeyCode.T, PressType.DOWN, Toggle);
        Exit();
    }


    public void Enter()
    {
        events.CallEvent("Enter");
    }

    public void Exit()
    {
        events.CallEvent("Exit");
    }

    public void Toggle()
    {
        toggle = !toggle;
        if (toggle) {
            Enter();
        } else {
            Exit();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
