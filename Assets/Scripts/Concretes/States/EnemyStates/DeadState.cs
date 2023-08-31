using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState
{
    IEnemyController _enemyController;

    public DeadState(IEnemyController enemyController)
    {
        _enemyController = enemyController;
    }
    public void OnEnter()
    {
        Debug.Log($"{nameof(DeadState)} {nameof(OnEnter)}");
        _enemyController.Animation.DeadAnimation("IsDead");
        _enemyController.Dead.DeadAction();
    }

    public void OnExit()
    {
        Debug.Log($"{nameof(DeadState)} {nameof(OnExit)}");
    }

    public void Tick()
    {
        return;
    }

    public void FixedTick()
    {
        return;
    }
    public void LateTick()
    {
        return;
    }
}
