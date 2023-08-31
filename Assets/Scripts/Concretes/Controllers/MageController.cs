using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageController : IEnemyController
{
    public Transform transform { get; }
    public bool IsFacingRight { get; }
    public Transform Transform { get; }
    public IAttack Attacker { get; }
    public IHealth Health { get; }
    public IMoveDal MoveManager { get; }
    public IAnimation Animation { get; }
    public Transform Target { get; set; }
    public float Magnitude { get; }
    public Dead Dead { get; }
    public void FindNearestTarget()
    {
        throw new System.NotImplementedException();
    }

    public void FindClosestEnemyOnLayer(int layer)
    {
        throw new System.NotImplementedException();
    }

    public LayerMask EnemyLayerMask { get; }
    public bool CanAttack { get; }
}
