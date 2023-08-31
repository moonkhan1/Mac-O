using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementsService
{
    void Tick();
    void FixedTick();
}
