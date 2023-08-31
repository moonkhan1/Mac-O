using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IPlayerMission
{
    MissionManager MissionManager { get; set; }
    void Tick();
}
