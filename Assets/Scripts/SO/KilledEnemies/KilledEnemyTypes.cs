using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class KilledEnemyTypes : MMSingleton<KilledEnemyTypes>
{
    private Dictionary<EnemyTypes, int> _killedEnemiesCount;

    public enum EnemyTypes
    {
        Worm,
        Barbarian,
        Mage,
        Unknown
    }

    protected override void Awake()
    {
        _killedEnemiesCount = new();
    }

    public void RecordKilledEnemy(IEnemyController enemyController)
    {
        var enemy = DetermineEnemyType(enemyController);
        if (_killedEnemiesCount.ContainsKey(enemy))
        {
            _killedEnemiesCount[enemy]++;
        }
        else
        {
            _killedEnemiesCount.Add(enemy, 1);
        }
    }

    public int GetKilledCount()
    {
        return _killedEnemiesCount.Count;
    }

    public void ResetKillCount(EnemyTypes enemyTypes)
    {
        _killedEnemiesCount[enemyTypes] = 0;
    }

    public int GetKilledEnemyCount(EnemyTypes enemyType)
    {
        if (_killedEnemiesCount.TryGetValue(enemyType, out var count))
        {
            return count;
        }
        else
        {
            return 0; 
        }
    }
    
    private EnemyTypes DetermineEnemyType(IEnemyController enemyController)
    {
        if (enemyController is WormController)
        {
            return EnemyTypes.Worm;
        }
    
        if (enemyController is MageController)
        {
            return EnemyTypes.Mage;
        }
        if (enemyController is BarbarController)
        {
            return EnemyTypes.Barbarian;
        }
        else
        {
            return EnemyTypes.Unknown;
        }
    }
}

