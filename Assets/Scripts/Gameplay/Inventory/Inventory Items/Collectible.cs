using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    public virtual string Name { get; }
    public uint Count = 1;
    [Min(0f)] public float Weight = 1.0f;

    public override bool Equals(object obj)
    {
        Collectible other = obj as Collectible;
        return other != null && other.Name == this.Name;
    }

    public override int GetHashCode()
    {
        if (Name == null) return 0;
        return Name.GetHashCode();
    }
}
