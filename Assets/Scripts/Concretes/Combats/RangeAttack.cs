using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class RangeAttack : IAttack
{
    private readonly IPlayerController _playerController;
    private bool canFire = true;
    private Collider2D[] colliderList;

    public int Damage { get; set; }
    public RangeAttack(IPlayerController playerController)
    {
        _playerController = playerController;
        Damage = playerController.Damage;
    }
    public void AttackAction(IAttack attack)
    {
        LayerMask enemyLayer = 6;
        var weaponInHand = _playerController.PlayerWeaponInHand;
        var weapon = _playerController.PlayerWeapon;
        Vector3 playerPos = _playerController.transform.position;
        float offset = _playerController.transform.GetChild(0).localScale.x;
        
        Collider2D isInRangeObject = FindClosestObjectOnLayer(enemyLayer);
        if(isInRangeObject == null) return;
        
        IEnemyController enemyController = isInRangeObject.GetComponent<IEnemyController>();
        
        if (_playerController.InputReader.isInteraction && canFire && !_playerController.CanInteractWithObject 
            && !enemyController.Health.IsDead)
        {
            _playerController.IsPlayerAttacking = true;
            canFire = false;
            weaponInHand.gameObject.SetActive(false);
            var seq = DOTween.Sequence();
            GameObject weaponNew = Object.Instantiate(weapon, weaponInHand.transform.position, Quaternion.Euler(0, 0, 0));
            weaponNew.transform.parent = null;
            
            Vector3 throwDirection = Vector3.right * offset; // Initial throw direction
            if (_playerController.transform.localScale.x < 0)
            {
                throwDirection = Vector3.left * offset;
            }
            weaponNew.transform.DORotate(new Vector3(0, 0, -45), 0.1f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Incremental).SetRelative();
            seq.Append(weaponNew.transform.DOMove(isInRangeObject.transform.position, 0.15f));
            enemyController.Health.TakeDamage(Damage);
            
            var enemySeq = DOTween.Sequence();
            var mat = isInRangeObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material;
            var defaultColor = mat.color;
            var damageColor = Color.red;
            var fadedColor = new Color(damageColor.r, damageColor.g, damageColor.b, 0.8f); // Set alpha to 0.5 (adjust as needed)
            
            Vector3 backwardDirection = enemyController.IsFacingRight ? Vector2.left : Vector2.right;
            enemyController.Transform.DOJump(enemyController.Transform.position + backwardDirection, 0.5f, 1, 0.6f);
            
            enemySeq.Append(mat.DOColor(fadedColor,0.2f)).Join(isInRangeObject.transform.DOShakeScale(0.1f, 1f));
            enemySeq.Append(mat.DOColor(defaultColor,0.2f));
            enemySeq.OnComplete(() => {
                isInRangeObject.transform.DOScale(1f, 0.1f);
            });
            
            enemySeq.Append(weaponNew.transform.DOLocalMove(playerPos+throwDirection,0.18f));

            enemySeq.OnComplete(() => {
                Object.Destroy(weaponNew);
                weaponInHand.gameObject.SetActive(true);
                canFire = true;
                _playerController.IsPlayerAttacking = false;
            });
            if (enemyController.Health.IsDead)
            {
                KilledEnemyTypes.Instance.RecordKilledEnemy(enemyController);
                KilledEnemyTypes.Instance.GetKilledCount();
            }
        }

    }
    private Collider2D FindClosestObjectOnLayer(int layer)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(_playerController.transform.position, new Vector2(10, 10), 0, 1 << layer);
        return colliders.FirstOrDefault();
    }
    
}


