using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlacement : MonoBehaviour
{
    RectTransform rect = null;
    Vector3 basePos = Vector3.zero;

    private void Start() {
        rect = GetComponent<RectTransform>();
        basePos = rect.position;
    }

    public void Enter()
    {
        rect.position = basePos;

    }
    public void Exit()
    {
        rect.position = basePos + Vector3.up * 136;
    }
}
