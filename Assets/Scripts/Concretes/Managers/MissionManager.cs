using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public List<MissionSO> allMissions = new();
    public List<MissionSO> activeMissions = new();
    public List<MissionSO> completedMissions = new();
    public static event Action<MissionSO> OnMissionCompleted;
    public static event Action<MissionSO> OnMissionStarted;

    private void Start()
    {
        foreach (var mission in allMissions)
        {
            mission.isCompleted = false;
            mission.isActive = false;
            if (mission is KillMission killMission)
            {
                killMission.currentKills = 0;
            }
        }
    }

    public void StartMission(MissionSO mission)
    {
        activeMissions.Add(mission);
        mission.isActive = true;
        CheckBlockedPaths();

    }
    
    public void StartMission(params MissionSO[] missions)
    {
        foreach (var mission in missions)
        {
            activeMissions.Add(mission);
            mission.isActive = true;
            OnMissionStarted?.Invoke(mission);
        }
        CheckBlockedPaths();
    }
    public void UpdateMissionProgress(MissionSO mission)
    {
        mission.UpdateProgress();
        if (mission.IsCompleted())
        {
            CompleteMission(mission);
            CheckBlockedPaths();

        }
    }
    private void CompleteMission(MissionSO mission)
    {
        mission.isActive = false;
        mission.isCompleted = true;
        activeMissions.Remove(mission);
        completedMissions.Add(mission);
        OnMissionCompleted?.Invoke(mission);
    }
    private void CheckBlockedPaths()
    {
        PathBlocker[] pathBlockers = FindObjectsOfType<PathBlocker>();
        foreach (PathBlocker blocker in pathBlockers)
        {
            if (blocker.requiredMission != null && blocker.requiredMission.isCompleted)
            {
                blocker.UnlockPath();
            }
        }
    }
}
