using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    public override Vector3 getInput()
    {
        if (!enabled)
            return Vector3.zero;
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
    }
}
