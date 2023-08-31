using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputReader 
{ 
    float Horizontal { get; }
    bool isJump { get; }
    bool isMovingPressed{get;}
    bool isInteraction { get; }
    bool isDash { get; }

}
