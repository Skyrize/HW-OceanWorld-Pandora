using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InputEvent {
    [SerializeField] protected KeyCode key = KeyCode.A;
    [SerializeField] public UnityEvent onKeyDown = new UnityEvent();
    [SerializeField] public UnityEvent onKeyUp = new UnityEvent();
    [SerializeField] public UnityEvent onKey = new UnityEvent();

    public void Process()
    {
        if (Input.GetKey(key)) {
            onKey.Invoke();
        }
        if (Input.GetKeyDown(key)) {
            onKeyDown.Invoke();
        }
        if (Input.GetKeyUp(key)) {
            onKeyUp.Invoke();
        }
    }
}

public class InputManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField]
    protected InputEvent[] inputEvents;

    // Update is called once per frame
    void Update()
    {
        foreach (InputEvent item in inputEvents)
        {
            item.Process();
        }
    }
}
