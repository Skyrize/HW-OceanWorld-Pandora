using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatDestruction : MonoBehaviour
{
    public GameObject Kaboom = null;
    public void DestroyBoat()
    {
        Kaboom.transform.parent = null;
        Kaboom.SetActive(true); // boat go boom
    }
}
