using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PositionBinder : MonoBehaviour
{
    public Transform target = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool toggle = false;
    // Update is called once per frame
    void Update()
    {
        if (target)
            this.transform.position = target.position;
        if (Input.GetKeyDown(KeyCode.T)) {
            toggle = !toggle;
            if (toggle) {
                GetComponent<TweenAnimator>().Play(0);
            } else {
                GetComponent<TweenAnimator>().Rewind(0);
            }
        }
    }
}
