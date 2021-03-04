using UnityEngine;

public class Collectable : MonoBehaviour
{
    public string Name;
    public uint Count = 1;
    [Min(0f)] public float Weight = 1.0f;
}