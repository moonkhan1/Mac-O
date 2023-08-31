using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health info", menuName = "Info/Health Information", order = 51)]
public class HealthSO : ScriptableObject
{
    [SerializeField] private int _maxHealth;
    public int MaxHealth => _maxHealth;
}
