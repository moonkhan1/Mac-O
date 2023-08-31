using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITalkableNPC
{ 
    Dialogue Dialogue { get; }
    event Action OnPlayerApproached;
}
