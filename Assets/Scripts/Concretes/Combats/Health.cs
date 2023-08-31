using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : IHealth
{
    private int _currentHealth;
    private int _maxHealth;

    public int CurrentHealth => _currentHealth;
    public bool IsDead => _currentHealth <= 0;
    public event Action OnTakeDamage;
    public event Action OnDead;

    public Health(HealthSO healthSo)
    {
        _maxHealth = healthSo.MaxHealth;
        _currentHealth = _maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        if (IsDead) return;
        
        _currentHealth -= damage; 
        _currentHealth = Mathf.Max(_currentHealth, 0);
        OnTakeDamage?.Invoke();
        if(IsDead)
        {
            OnDead?.Invoke();
        }
    }    
}
