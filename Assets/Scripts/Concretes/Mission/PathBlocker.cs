using UnityEngine;

public class PathBlocker : MonoBehaviour
{
    public MissionSO requiredMission;
    public Collider2D pathCollider;


    private void Start()
    {
        MissionManager.OnMissionCompleted += HandleMissionCompletion;
        UpdatePathBlocker();
        pathCollider.enabled = true;
    }

    private void HandleMissionCompletion(MissionSO completedMission)
    {
        if (completedMission == requiredMission)
        {
            UpdatePathBlocker();
        }
    }

    private void UpdatePathBlocker()
    {
        bool isBlocked = requiredMission != null && !requiredMission.isCompleted;
        if(pathCollider == null) return;
        pathCollider.enabled = isBlocked;

    }
    public void UnlockPath()
    {
        pathCollider.enabled = false;
    }
}

