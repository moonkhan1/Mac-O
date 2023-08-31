using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Traps info", menuName = "Info/Trap Information", order = 51)]
public class TrapsSO : ScriptableObject
{
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _layerMask;

    public int Damage => _damage;
    public LayerMask Layer => _layerMask;
}
