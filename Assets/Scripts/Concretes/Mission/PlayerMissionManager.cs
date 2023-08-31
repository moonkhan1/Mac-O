using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerMissionManager : IPlayerMission
{
    readonly IPlayerController _playerController;
    public MissionManager MissionManager { get; set; }

    public PlayerMissionManager(IPlayerController playerController)
    {
        _playerController = playerController;
        MissionManager = _playerController.MissionManager;
    }
    public void Tick()
    {
        if (MissionManager.activeMissions.Count == 0) return;

        var mission = MissionManager.activeMissions.Find(m => m.isActive);
        if (mission != null)
        {
            MissionManager.UpdateMissionProgress(mission);
        }
    }
}
