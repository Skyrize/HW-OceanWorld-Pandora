using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PostEvent : UnityEvent<Post>
{
}

public class Post : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected float hireTime = 1f;
    public float HireTime => hireTime;
    [Header("Events")]
    [SerializeField] public UnityEvent onHire = new UnityEvent();
    [SerializeField] public UnityEvent onFire = new UnityEvent();
    [Header("References")]
    [SerializeField] protected CrewMember employee = null;
    [SerializeField] public CrewMember Employee => employee;
    [SerializeField] protected Transform postPlace = null;
    [SerializeField] protected bool working = false;

    WaitForSeconds timer;

    private void Awake() {
        hireTime = 0;
        timer = new WaitForSeconds(hireTime);
    }

    IEnumerator _SetEmployee(CrewMember newEmployee)
    {
        employee = newEmployee;
        employee.Hire(this, "Working", hireTime);
        var employeeObject = Instantiate(employee.prefab, postPlace.position, postPlace.rotation, postPlace);
        yield return timer;
        working = true;
        
        onHire.Invoke();
    }

    public void ForceHire(CrewMember newEmployee)
    {
        ClearEmployee();
        timer = null;
        var tmp = hireTime;
        hireTime = 0;
        StartCoroutine(_SetEmployee(newEmployee));
        hireTime = tmp;
        timer = new WaitForSeconds(hireTime);
    }

    public void ClearEmployee()
    {
        onFire.Invoke();
        postPlace.ClearChilds(); // bof
        if (employee) {
            employee.Fire();
        }
        employee = null;
        working = false;
    }

    public void SetEmployee(CrewMember newEmployee)
    {
        ClearEmployee();
        //TODO : Status in employee here ?
        StartCoroutine(_SetEmployee(newEmployee));
    }

    virtual protected void _Use()
    {
        throw new System.Exception("Missing implementation");
    }
    virtual protected void _Use(Vector3 target)
    {
        throw new System.Exception("Missing implementation");
    }

    public void Use(Vector3 target)
    {
        if (working)
            _Use(target);
    }

    public void Use()
    {
        if (working)
            _Use();
    }
}
