using System.Collections;
using System.Collections.Generic;
using Concretes.Combats;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack info", menuName = "Info/Attack Information", order = 51)]
public class AttackSO : ScriptableObject
{
    enum AttackTypeEnum : byte
    {
        Worm, Mage, Barbar
    }
    [SerializeField] AttackTypeEnum _attackType;
    
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDelayTime;
    [SerializeField] private Vector2 _attackRange;

    public int Damage => _damage;
    public float AttackDelayTime => _attackDelayTime;
    public Vector2 AttackRange => _attackRange;
    
    public IAttack GetTypeOfAttack(IEnemyController enemyController)
    {
        switch (_attackType)
        {
            case AttackTypeEnum.Worm:
                return new WormAttack(enemyController, this);
            case AttackTypeEnum.Mage:
                return new MageAttack(enemyController, this);
            case AttackTypeEnum.Barbar:
                return new BarbarAttack(enemyController, this);
            default:
                return null;
        }
    }
}

