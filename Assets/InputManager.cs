using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InputAxisEvent : UnityEvent<float>
{
}

[System.Serializable]
public class KeyEvent {
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

[System.Serializable]
public class MouseButtonEvent {
    [HideInInspector] protected int button = 0;
    [SerializeField] public UnityEvent onButtonDown = new UnityEvent();
    [SerializeField] public UnityEvent onButtonUp = new UnityEvent();
    [SerializeField] public UnityEvent onButton = new UnityEvent();

    public void Process()
    {
        if (Input.GetMouseButton(button)) {
            onButton.Invoke();
        }
        if (Input.GetMouseButtonDown(button)) {
            onButtonDown.Invoke();
        }
        if (Input.GetMouseButtonUp(button)) {
            onButtonUp.Invoke();
        }
    }

    public MouseButtonEvent(int button)
    {
        this.button = button;
    }
}

[System.Serializable]
public class AxisEvent {
    [SerializeField] protected string axis = "Horizontal";
    [SerializeField] protected bool raw = false;
    [SerializeField] public InputAxisEvent onAxisEvent = new InputAxisEvent();

    public void Process()
    {
        if (raw) {
            onAxisEvent.Invoke(Input.GetAxisRaw(axis));
        } else {
            onAxisEvent.Invoke(Input.GetAxis(axis));
        }
    }
    
    public AxisEvent(string axis = "", bool raw = false)
    {
        this.axis = axis;
        this.raw = raw;
    }
}

[System.Serializable]
public class MouseEvent {
    [SerializeField] public MouseButtonEvent leftButtonEvents = new MouseButtonEvent(0);
    [SerializeField] public MouseButtonEvent rightButtonEvents = new MouseButtonEvent(1);
    [SerializeField] public MouseButtonEvent middleButtonEvents = new MouseButtonEvent(2);
    [SerializeField] public AxisEvent scrollEvents = new AxisEvent("Mouse ScrollWheel", true);

    public void Process()
    {
        leftButtonEvents.Process();
        rightButtonEvents.Process();
        middleButtonEvents.Process();
        scrollEvents.Process();
    }
}

public class InputManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField]
    public MouseEvent mouseEvents = new MouseEvent();
    [SerializeField]
    public KeyEvent[] keyEvents;
    [SerializeField]
    public AxisEvent[] axisEvents;

    // Update is called once per frame
    void Update()
    {
        mouseEvents.Process();
        foreach (KeyEvent item in keyEvents)
        {
            item.Process();
        }
        foreach (AxisEvent item in axisEvents)
        {
            item.Process();
        }
    }

    private static InputManager instance = null;
    public static InputManager Instance {
        get {
            if (!instance) {
                Debug.LogException(new MissingReferenceException("InputManager not initialized. Be careful to ask for instance AFTER Awake method."));
            }
            return instance;
        }
        set {
            if (instance != null)
                Debug.LogException(new System.Exception("More than one InputManager in the scene. Please remove the excess"));
            instance = value;
        }
    }
            
    private void Awake() {
        Instance = this;
    }
}
