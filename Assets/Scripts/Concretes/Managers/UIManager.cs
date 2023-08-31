using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using Zenject;

public class UIManager : MMSingleton<UIManager>
{
    [SerializeField] private GameObject MissionPanel;
    [SerializeField] private TextMeshProUGUI MissionPanelDescriptionText;
    [Inject] private MissionManager missionManager;

    private void Start()
    {
        MissionPanel.SetActive(false);
        MissionManager.OnMissionCompleted += MissionCompletion;
    }
    private void OnDisable()
    {
        MissionManager.OnMissionCompleted -= MissionCompletion;
    }
    
    private void MissionCompletion(MissionSO obj)
    {
        MissionPanelDescriptionText.text = obj.description;
        MissionPanel.SetActive(true);
    }

    public void CloseMissionPanel()
    {
        MissionPanel.SetActive(false);
    }
}
