using DG.Tweening;
using UnityEngine;

namespace Concretes.Traps
{
    public class WaterPoolController : TrapsBase
    {
        [SerializeField] private TrapsSO _trapsSo;
        private IPlayerController _playerController;
        protected override TrapsSO TrapsSo => _trapsSo;
        
        public WaterPoolController(int damage, LayerMask layer) : base(damage, layer)
        {
        }
        protected override void OnCollisionEnter2D(Collision2D col)
        {
            if(((1<<col.gameObject.layer) & Layer) == 0) return;
            IPlayerController playerController = col.transform.GetComponent<IPlayerController>();
            playerController.Health.TakeDamage(Damage);
            Vector3 backwardDirection = playerController.IsFacingRight ? Vector2.left : Vector2.right;
            playerController.transform.DOLocalJump(playerController.transform.position, 1.5f, 1, 0.6f);
        }
    }
}