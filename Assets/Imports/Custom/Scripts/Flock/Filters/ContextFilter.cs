using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContextFilter : ScriptableObject
{

    public abstract List<Transform> filter(in FlockAgent agent, in List<Transform> originalContext);
}
