using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CollisionEvent : UnityEvent<GameObject>
{
}

public class ColliderEvent : MonoBehaviour
{
    [Header("Filters")]
    [SerializeField] protected bool useTagFilter = false;
    [SerializeField] protected bool useLayerFilter = false;
    [SerializeField] protected bool useComponentFilter = false;
    [SerializeField] protected bool useParentComponentFilter = false;
    [SerializeField] protected bool useChildComponentFilter = false;
    public bool UseTagFilter { get { return useTagFilter; } }
    public bool UseLayerFilter { get { return useLayerFilter; } }
    public bool UseComponentFilter { get { return useComponentFilter; } }
    public bool UseParentComponentFilter { get { return useParentComponentFilter; } }
    public bool UseChildComponentFilter { get { return useChildComponentFilter; } }

    [SerializeField]
    private List<string> tags = null;

    [SerializeField]
    private LayerMask layerMask = 1;

    [SerializeField]
    private string componentType = "Transform";

    [SerializeField]
    private string parentComponentType = "Transform";

    [SerializeField]
    private string childComponentType = "Transform";


    [Header("Events")]
    [SerializeField] private CollisionEvent onCollide = new CollisionEvent();

    public void ValidateType(string type)
    {
        if (Type.GetType(type) == null) {
            type = "UnityEngine." + type + ", UnityEngine";
            if (Type.GetType(type) == null) {
                throw new Exception("Invalid Type " + type + " on GameObject " + gameObject.name + ".");
            }
        }
    }

    private void Awake() {
        if (useComponentFilter) {
            ValidateType(componentType);
        }
        if (useParentComponentFilter) {
            ValidateType(parentComponentType);
        }
        if (useChildComponentFilter) {
            ValidateType(childComponentType);
        }
    }

    public bool HasTag(GameObject target)
    {
        return tags.Contains(target.tag);
    }
    
    public bool HasLayer(GameObject target)
    {
        return ((1 << target.layer) & layerMask) != 0;
    }

    public bool HasComponent(GameObject target)
    {
        return target.GetComponent(Type.GetType(componentType)) != null;
    }
    
    public bool HasParentComponent(GameObject target)
    {
        return target.GetComponentInParent(Type.GetType(componentType)) != null;
    }
    
    public bool HasChildComponent(GameObject target)
    {
        return target.GetComponentInChildren(Type.GetType(componentType)) != null;
    }

    public void FilterCollision(GameObject target)
    {
        if (useTagFilter && !HasTag(target)) {
            return;
        }
        if (useLayerFilter && !HasLayer(target)) {
            return;
        }
        if (useComponentFilter && !HasComponent(target)) {
            return;
        }
        if (useParentComponentFilter && !HasParentComponent(target)) {
            return;
        }
        if (useChildComponentFilter && !HasChildComponent(target)) {
            return;
        }
        onCollide.Invoke(target);
    }
    
    private void OnCollisionEnter(Collision other) {
        FilterCollision(other.gameObject);
    }
    private void OnTriggerEnter(Collider other) {
        FilterCollision(other.gameObject);
    }
}
