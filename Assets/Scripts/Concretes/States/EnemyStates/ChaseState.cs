using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    float _speed = 10f;
    IEnemyController _enemyController;

    public ChaseState(IEnemyController enemyController)
    {
        _enemyController = enemyController;
    }
    public void OnEnter()
    {
        Debug.Log($"{nameof(ChaseState)} {nameof(OnEnter)}");
    }

    public void OnExit()
    {
        Debug.Log($"{nameof(ChaseState)} {nameof(OnExit)}");
        _enemyController.MoveManager.MoveAction(_enemyController.Transform.position, 0);
        //SoundManager.Instance.EnemyMoveSound.Stop();
    }

    public void Tick()
    {
        _enemyController.MoveManager.MoveAction(_enemyController.Target.position, _speed);
        //if(SoundManager.Instance.EnemyMoveSound.isPlaying) return;
        //SoundManager.Instance.EnemyMoveSound.Play();
    }
    public void FixedTick()
    {
        //_enemyController.FindNearestTarget();
    }
    public void LateTick()
    {
        var isMoving = _enemyController.Magnitude > 0f;
        _enemyController.Animation.RunAnimation(isMoving);
    }
}
