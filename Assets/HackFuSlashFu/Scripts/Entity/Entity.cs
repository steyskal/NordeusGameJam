﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class Entity : MonoBehaviour
{
    public int InitialHealth = 10;
    public int MaxHealth = 10;

    [Header("Read-Only")]
    [SerializeField]
    private int _currentHealth;

    [SerializeField]
    public UnityEvent OnEntityDie = new UnityEvent();

    public int CurrentHealth
    {
        get
        {
            return _currentHealth;
        }

        set
        {
            _currentHealth = value <= MaxHealth ? value : MaxHealth;

            if (_currentHealth <= 0)
                Die();
        }
    }

    protected virtual void Awake()
    {
        CurrentHealth = InitialHealth;
    }
    /// <summary>
    /// Applies damage on player and returns true if player will die
    /// </summary>
    /// <param name="damage"> Damage dealt to enemy</param>
    /// <param name="comboBonus">Score bonus gained from combo</param>
    /// <returns></returns>
    public virtual bool ApplyDamage(int damage, int comboBonus = 0)
    {
        CurrentHealth -= Mathf.Abs(damage);
        if (_currentHealth <= 0)
        {
            OnEntityDie.Invoke();
            return true;
        }
        return false;
    }

    public virtual void Die()
    {
        Debug.Log(gameObject.name + " died.");

        Destroy(gameObject);
    }
}