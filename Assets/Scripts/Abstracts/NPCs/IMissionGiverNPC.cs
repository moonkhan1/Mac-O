using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMissionGiverNPC 
{
  MissionManager MissionManager { get; }
  MissionSO[] MissionSo { get; }
}
