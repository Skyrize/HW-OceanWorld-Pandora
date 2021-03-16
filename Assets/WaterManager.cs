using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    [Header("Settings")]
    [Min(1)]
    [SerializeField] public uint xSize = 1 , zSize = 1;
    [Header("References")]
    [SerializeField] public GameObject waterPrefab = null;

}
