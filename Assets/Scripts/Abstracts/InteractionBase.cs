using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionBase : MonoBehaviour
{
    public abstract void Interact(Transform interactionUI, Transform rayCastPoint);
    public abstract void SwingInteract();
    public bool IsHolding = false;
    public bool IsSwinging = false;
}
