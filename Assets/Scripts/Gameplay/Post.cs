using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] protected Transform postPlace = null;

    WaitForSeconds timer;

    private void Awake() {
        timer = new WaitForSeconds(hireTime);
    }

    IEnumerator _SetEmployee(CrewMember newEmployee)
    {
        yield return timer;
        employee = newEmployee;
        var employeeObject = Instantiate(employee.prefab, postPlace.position, postPlace.rotation, postPlace);

        //TODO : Status in employee here ?
    }

    void ClearEmployee()
    {
        postPlace.ClearChilds(); // bof
        employee = null;
    }

    public void SetEmployee(CrewMember newEmployee)
    {
        ClearEmployee();
        onFire.Invoke();
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
        if (employee)
            _Use(target);
    }

    public void Use()
    {
        if (employee)
            _Use();
    }
}
