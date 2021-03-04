using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject debugArrowPrefab = null;
    static private DebugManager _instance = null;
    static public DebugManager instance {
        get {
            if (_instance == null)
                Debug.LogException(new System.Exception("Asking for instance too early (awake)"));
            return DebugManager._instance;
        }

        set {
            if (_instance) {
                Debug.LogException(new System.Exception("More than one DebugManager in the Scene"));
            } else {
                _instance = value;
            }
        }
    }
    private void Start() {
        instance = this;
    }
    public void CreateDebugArrow(ContactPoint contactPoint, bool reverse = false)
    {
        if (reverse) {
            CreateDebugArrow(contactPoint.point, contactPoint.point - contactPoint.normal);
        } else {
            CreateDebugArrow(contactPoint.point, contactPoint.point + contactPoint.normal);
        }
    }

    public void CreateDebugArrow(Vector3 position, Vector3 lookAt)
    {
        var arrow = GameObject.Instantiate(debugArrowPrefab, position, Quaternion.identity, transform);

        arrow.transform.LookAt(lookAt);
    }

    public void Log(string message)
    {
        Debug.Log(message);
    }

}
