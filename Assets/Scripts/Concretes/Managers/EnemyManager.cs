using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // [SerializeField] int _maxEnemyCountOnRound = 30;

    public List<PlayerOneController> Targets;
    public List<IEnemyController> Enemies { get; private set; }

    public bool IsListEmpty => Enemies.Count <= 0; 
    // public bool CanSpawnEnemy => _maxEnemyCountOnRound > _enemies.Count;

    void Awake() 
    {
        Enemies = new List<IEnemyController>();
        Targets = new List<PlayerOneController>();
    }
    public void AddEnemyToList(IEnemyController enemyController)
    {
        enemyController.transform.parent = transform;
        Enemies.Add(enemyController);
    }

    public void RemoveEnemyFromList(IEnemyController enemyController)
    {
        Enemies.Remove(enemyController);
    }
    
}
