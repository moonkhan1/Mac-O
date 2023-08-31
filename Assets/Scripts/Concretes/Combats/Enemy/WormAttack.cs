using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Concretes.Combats
{
    public class WormAttack : IAttack
    {
        public int Damage { get; set; }
        private readonly IEnemyController _enemyController;
        private readonly float _attackDelayTime;
        private float _currentTime = 0f;
        private bool _canAttack;

        public WormAttack(IEnemyController enemyController, AttackSO attackSo)
        {
            _enemyController = enemyController;
            Damage = attackSo.Damage;
            _attackDelayTime = attackSo.AttackDelayTime;
        }
        
        public void AttackAction(IAttack attack)
        {
            _currentTime += Time.deltaTime;
            _canAttack = _currentTime > _attackDelayTime;
            
           // Collider2D isInRangeObject = FindClosestObjectOnLayer(playerLayer);
            if(_enemyController.Target == null) return;
            IPlayerController playerController = _enemyController.Target.GetComponent<IPlayerController>();
            if(playerController == null) return;
            if (!_enemyController.CanAttack || !_canAttack) return;

            playerController.Health.TakeDamage(Damage);
            Vector3 backwardDirection = playerController.IsFacingRight ? Vector2.left : Vector2.right;
            playerController.transform.DOLocalJump(playerController.transform.position + backwardDirection * 5f, 1.5f,
                1, 0.6f);
            _currentTime = 0f;
        }
        
        // private Collider2D FindClosestObjectOnLayer(int layer)
        // {
        //     Collider2D[] colliders = Physics2D.OverlapBoxAll(_enemyController.Transform.position, new Vector2(3, 3), 0, 1 << layer);
        //     _enemyController.Target = colliders.FirstOrDefault()?.transform;
        //     return colliders.FirstOrDefault();
        // }

    }
}