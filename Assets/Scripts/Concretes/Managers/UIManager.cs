using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class UIManager : MMSingleton<UIManager>
{
    [SerializeField] private GameObject MissionPanel;
    [SerializeField] private TextMeshProUGUI MissionPanelDescriptionText;
    [Inject] private InputOneReader inputOneReader;
    [Inject] private InputTwoReader inputTwoReader;

    
    [SerializeField] private GameObject FinalPanel;

    private void Start()
    {
        MissionPanel.SetActive(false);
        FinalPanel.SetActive(false);
        MissionManager.OnMissionCompleted += MissionCompletion;
        // inputOneReader.isInteraction += CloseMissionPanel();
    }
    private void OnDisable()
    {
        MissionManager.OnMissionCompleted -= MissionCompletion;
    }
    
    private void MissionCompletion(MissionSO obj)
    {
        MissionPanelDescriptionText.text = obj.description;
        MissionPanel.SetActive(true);
        CloseMissionPanel();
    }

    private async void CloseMissionPanel()
    {
        await Task.Delay(3500);
        //await Task.WhenAll();
        MissionPanel.SetActive(false);
    }

    public void ActivateFinalMessage()
    {
        FinalPanel.SetActive(true);
    }
}
