using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractionService 
{
    void Tick();
    void FixedTick();
    void LateTick();
}
