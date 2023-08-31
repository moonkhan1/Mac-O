using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class MissionSO : ScriptableObject
{
    public string missionName;
    public string description;
    public bool isActive;
    public bool isCompleted;
    public abstract bool IsCompleted();
    public abstract void UpdateProgress();
}

