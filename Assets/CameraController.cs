﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform followTarget = null;
    [SerializeField] protected Camera cam = null;

    protected Plane seeLevel = new Plane(Vector3.up, Vector3.zero);
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }
    void Focus()
    {
        if (!followTarget)
            return;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        float distance;
        seeLevel.Raycast(ray, out distance);
        Vector3 move = followTarget.position - ray.direction * distance;
        move.y = cam.transform.position.y;
        cam.transform.position = move;
    }

    // Update is called once per frame
    void Update()
    {
        Focus();
    }
}
