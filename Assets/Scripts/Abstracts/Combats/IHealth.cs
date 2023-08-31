using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth 
{
    bool IsDead{get;}
    int CurrentHealth { get; } 
    void TakeDamage(int Damage);
    event System.Action OnTakeDamage;
    event System.Action OnDead;
}
