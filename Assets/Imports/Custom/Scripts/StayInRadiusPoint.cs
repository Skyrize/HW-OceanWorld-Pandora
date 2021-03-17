using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInRadiusPoint : MonoBehaviour
{
    [SerializeField] protected StayInRadiusBehavior behavior;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        behavior.centerPoint = transform.position;
        
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, behavior.radius);
    }
}
