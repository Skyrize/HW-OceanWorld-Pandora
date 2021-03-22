using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;    
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
