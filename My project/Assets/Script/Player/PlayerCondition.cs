using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDamagable
{
    void TakePhysicalDamage(int damage);
}
public class PlayerCondition : MonoBehaviour, IDamagable
{
    // Start is called before the first frame update
    public ConditionUI conditionUI;

    Condition health { get { return conditionUI.health; } }

    Condition stamina { get { return conditionUI.stamina; } }

    public float noHungerHeathDecay;

    public event Action onTakeDamage;
    // Update is called once per frame
    void Update()
    {
        stamina.Add(stamina.passiveValue * Time.deltaTime);
        health.Add(health.passiveValue * Time.deltaTime);

        if (health.curValue == 0)
        {
            Die();
        }
    }

    public void Heal(float value)
    {
        health.Add(value);
    }

    public void Die()
    {
        Debug.Log("»ç¸Á");
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
        if (health.curValue == 0)
        {
            Die();
        }
    }

    public bool CanJump()
    {
        if(stamina.curValue >= 10)
        {
            stamina.Subtract(10f);
            return true;
        }
        return false;
    }
}
