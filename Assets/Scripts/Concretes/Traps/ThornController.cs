using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ThornController : TrapsBase
{
    [SerializeField] private TrapsSO _trapsSo;
    private IPlayerController _playerController;
    protected override TrapsSO TrapsSo => _trapsSo;
    
    public ThornController(int damage, LayerMask layerMask) : base(damage, layerMask)
    {
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        if(((1<<col.gameObject.layer) & Layer) == 0) return;

        IPlayerController playerController = col.transform.GetComponent<IPlayerController>();
        playerController.Health.TakeDamage(Damage);
        Vector3 backwardDirection = playerController.IsFacingRight ? Vector2.left : Vector2.right;
        playerController.transform.DOLocalJump(playerController.transform.position + backwardDirection, 1.5f, 1, 0.6f);
    }

}
