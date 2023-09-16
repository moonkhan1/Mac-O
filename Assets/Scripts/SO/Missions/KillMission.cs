using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[CreateAssetMenu(menuName = "Missions/Kill Mission")]
public class KillMission : MissionSO
{
    public int targetKills;
    public int currentKills;
    public KilledEnemyTypes.EnemyTypes EnemyTypes;
    public override bool IsCompleted()
    {
        return currentKills >= targetKills;
    }
    
    public override void UpdateProgress()
    {
        int killedEnemyCount = KilledEnemyTypes.Instance.GetKilledEnemyCount(EnemyTypes);
        currentKills = killedEnemyCount;

        if (IsCompleted())
        {
            isCompleted = true;
            KilledEnemyTypes.Instance.ResetKillCount(EnemyTypes);
        }
    }
}

