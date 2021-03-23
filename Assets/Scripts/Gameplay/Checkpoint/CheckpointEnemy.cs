using System;
using UnityEngine;

public class CheckpointEnemy : MonoBehaviour
{
    public int ID;

    private void OnDestroy()
    {
        CheckpointData.Current.DefeatedEnemies.Add(ID);
        Debug.Log("hello");
    }
}