using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventBind {
    public string name;
    public float timer;
    public UnityEvent evt = new UnityEvent();
}

public class EventBinder : MonoBehaviour
{
    [SerializeField]
    protected EventBind[] events;

    private IEnumerator Delay(EventBind eventBinded)
    {
        yield return new WaitForSeconds(eventBinded.timer);
        eventBinded.evt.Invoke();
    }

    public void CallEvent(string eventName) {
        if (!isActiveAndEnabled)
            return;
        foreach (EventBind eventBind in events)
        {
            if (eventBind.name == eventName) {
                StartCoroutine(Delay(eventBind));
            }
        }
    }
}
