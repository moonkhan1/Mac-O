using UnityEngine;

namespace Concretes.States.EnemyStates
{
    public class IdleState : IState
    {
        IEnemyController _enemyController;

        public IdleState(IEnemyController enemyController)
        {
            _enemyController = enemyController;
        }
        public void OnEnter()
        {
            Debug.Log($"{nameof(IdleState)} {nameof(OnEnter)}");
            _enemyController.MoveManager.MoveAction(_enemyController.transform.position, 0);
        }

        public void OnExit()
        {
            Debug.Log($"{nameof(IdleState)} {nameof(OnExit)}");
            //_enemyController.MoveManager.MoveAction(_enemyController.Target.position, _speed);
            //SoundManager.Instance.EnemyMoveSound.Stop();
        }

        public void Tick()
        {
            //_enemyController.MoveManager.MoveAction(_enemyController.transform.position, 0);
            //if(SoundManager.Instance.EnemyMoveSound.isPlaying) return;
            //SoundManager.Instance.EnemyMoveSound.Play();
        }
        public void FixedTick()
        {
        }
        public void LateTick()
        {
            var isTargetInRange = _enemyController.Target == null;
            _enemyController.Animation.RunAnimation(!isTargetInRange);
        }
    }
}