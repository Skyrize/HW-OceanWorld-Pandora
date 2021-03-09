using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected float scrollSpeed = 200f;
    [SerializeField] protected float zoomMin = 15f;
    [SerializeField] protected float zoomMax = 100f;
    [Header("References")]
    [SerializeField] protected Camera cam = null;
    [Header("Runtime")]
    [SerializeField] public bool blockZoom = false;

    protected Plane seeLevel = new Plane(Vector3.up, Vector3.zero);
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    public void BlockZoom(bool value)
    {
        blockZoom = value;
    }

    void Focus(Transform target)
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        float distance;
        seeLevel.Raycast(ray, out distance);
        Vector3 move = target.position - ray.direction * distance;
        move.y = cam.transform.position.y;
        cam.transform.position = move;
    }

    private void OrthographicZoom(float input)
    {
        float zoom = -input * scrollSpeed;
        cam.orthographicSize += zoom;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, zoomMin, zoomMax);
    }

    private void PerspectiveZoom(float input)
    {
        Vector3 camPos = cam.transform.forward;
        Vector3 zoomDirection = -input * scrollSpeed * cam.transform.forward;
        
        cam.transform.Translate(zoomDirection, Space.World);
        camPos.y = Mathf.Clamp(camPos.y, zoomMin, zoomMax);
        transform.position = camPos;
    }

    public void Zoom(float input)
    {
        if (input == 0 || blockZoom)
            return;

        if (cam.orthographic) {
            OrthographicZoom(input);
        } else {
            PerspectiveZoom(input);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
