using System;
using DG.Tweening;
using UnityEngine;

public abstract class TrapsBase : MonoBehaviour
{
    protected abstract TrapsSO TrapsSo{ get; }
    protected int Damage
    {
        get => TrapsSo.Damage;
        set => throw new NotImplementedException();
    }

    protected LayerMask Layer
    {
        get => TrapsSo.Layer;
        set => throw new NotImplementedException();
    }

    protected TrapsBase(int damage, LayerMask layer)
    {
        Damage = damage;
        Layer = layer;
    }

    protected abstract void OnCollisionEnter2D(Collision2D col);
}
