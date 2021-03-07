using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatStats : MonoBehaviour
{
    public float hp = 100f;
    public void damage(float damage)
    {
        hp -= damage;
    }
}
