using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class UIManager : MMSingleton<UIManager>
{
    [SerializeField] private GameObject _missionNotificationPanel;
    [SerializeField] private TextMeshProUGUI _missionNotificationPanelDescriptionText;
    [SerializeField] private TextMeshProUGUI _missionMainPanelDescriptionText;
    [Inject] private InputOneReader inputOneReader;
    [Inject] private InputTwoReader inputTwoReader;

    
    [SerializeField] private GameObject FinalPanel;

    private void Start()
    {
        _missionNotificationPanel.SetActive(false);
        FinalPanel.SetActive(false);
        MissionManager.OnMissionCompleted += MissionCompletion;
        MissionManager.OnMissionStarted += MissionStarted;
        // inputOneReader.isInteraction += CloseMissionPanel();
    }
    private void OnDisable()
    {
        MissionManager.OnMissionCompleted -= MissionCompletion;
        MissionManager.OnMissionStarted -= MissionStarted;

    }

    private void MissionCompletion(MissionSO obj)
    {
        _missionNotificationPanelDescriptionText.text = obj.description;
        string originalText = _missionMainPanelDescriptionText.text;

        string modifiedText = originalText.Replace(obj.missionName, "");
        
        _missionMainPanelDescriptionText.text = modifiedText; 
        
        _missionNotificationPanel.SetActive(true);
        CloseMissionPanel();
    }

    private void MissionStarted(MissionSO obj)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append(_missionMainPanelDescriptionText.text);

        stringBuilder.AppendLine(obj.missionName);

        _missionMainPanelDescriptionText.text = stringBuilder.ToString();

    }

    private async void CloseMissionPanel()
    {
        await Task.Delay(3500);
        //await Task.WhenAll();
        _missionNotificationPanel.SetActive(false);
    }

    public void ActivateFinalMessage()
    {
        FinalPanel.SetActive(true);
    }
}
