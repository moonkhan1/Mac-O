using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    void AttackAction(IAttack attack);
    int Damage { get; set; }
}
