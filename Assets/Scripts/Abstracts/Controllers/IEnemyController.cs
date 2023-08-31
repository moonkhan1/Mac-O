using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemyController : IEntityController
{
    Transform Transform { get; }
    IAttack Attacker {get;}
    IHealth Health {get;}
    IMoveDal MoveManager{get;}
    IAnimation Animation { get; }
    Transform Target { get; set; }
    float Magnitude {get;}
    Dead Dead {get;}
    void FindNearestTarget();
    void FindClosestEnemyOnLayer(int layer);
    LayerMask EnemyLayerMask { get; }
    bool CanAttack { get; }
}
