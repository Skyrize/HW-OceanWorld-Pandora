using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DamageEvent : UnityEvent<float>
{
    public bool HasPersistentListener(UnityAction<float> call)
    {
        for (int i = 0; i != this.GetPersistentEventCount(); i++) {
            if (this.GetPersistentMethodName(i) == call.Method.Name)
                return true;
        }
        return false;
    }
}
[System.Serializable]
public class HealEvent : UnityEvent<float>
{
    public bool HasPersistentListener(UnityAction<float> call)
    {
        for (int i = 0; i != this.GetPersistentEventCount(); i++) {
            if (this.GetPersistentMethodName(i) == call.Method.Name)
                return true;
        }
        return false;
    }
}
[System.Serializable]
public class DeathEvent : UnityEvent<GameObject>
{
    public bool HasPersistentListener(UnityAction call)
    {
        for (int i = 0; i != this.GetPersistentEventCount(); i++) {
            if (this.GetPersistentMethodName(i) == call.Method.Name)
                return true;
        }
        return false;
    }
}
[System.Serializable]
public class LifeEvent : UnityEvent<float>
{
    public bool HasPersistentListener(UnityAction<float> call)
    {
        for (int i = 0; i != this.GetPersistentEventCount(); i++) {
            if (this.GetPersistentMethodName(i) == call.Method.Name)
                return true;
        }
        return false;
    }
}

public class HealthComponent : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private float _actualHealth = 100;
    [SerializeField]
    private float maxHealth = 100;

    [Header("Events")]
    public HealEvent onHealEvent = new HealEvent();
    public DamageEvent onDamageEvent = new DamageEvent();
    public DeathEvent onDeathEvent = new DeathEvent();
    public LifeEvent onLifeUpdate = new LifeEvent();

    private float actualHealth {
        get {
            return _actualHealth;
        }
        set {
            _actualHealth = Mathf.Clamp(value, 0, maxHealth);
            onLifeUpdate.Invoke(_actualHealth / maxHealth);
        }
    }

    public float Health {
        get {
            return _actualHealth;
        }
    }

    public float MaxHealth {
        get {
            return maxHealth;
        }
    }

    public bool IsAlive {
        get {
            return actualHealth > 0;
        }
    }
    public bool IsDead {
        get {
            return !IsAlive;
        }
    }

    public void ReduceHealth(float amount)
    {
        if (IsDead)
            return;
        actualHealth -= amount;
        onDamageEvent.Invoke(amount);
        if (IsDead)
            onDeathEvent.Invoke(gameObject);
    }
    
    public void IncreaseHealth(float amount)
    {
        if (IsDead)
            return;
        actualHealth += amount;
        onHealEvent.Invoke(amount);
    }

    public void Kill() {
        Destroy(this.gameObject);
    }

}
