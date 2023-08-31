using System.Collections;
using System.Collections.Generic;
using TwoD.Platformer;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTwoReader : IInputReader
{
    readonly PlayerOneInput _input;
    public float Horizontal { get; private set; }
    public bool isJump { get;  private set; }
    public bool isMovingPressed { get;  private set; }
    public bool isInteraction { get; private set; }
    public bool isDash { get; }

    public InputTwoReader()
    {
        _input = new PlayerOneInput();
        _input.Enable();
        _input.Player2.Move.performed += HandleMoveAction;
        _input.Player2.Move.canceled += HandleMoveAction;
        _input.Player2.Jump.performed += JumpPerformed;
        _input.Player2.Jump.canceled += JumpPerformed;
        _input.Player2.Interaction.started += InteractionPerformed;
        _input.Player2.Interaction.performed += InteractionPerformed;
        _input.Player2.Interaction.canceled += InteractionPerformed;
    }

    private void InteractionPerformed(InputAction.CallbackContext context)
    {
        isInteraction = context.ReadValueAsButton();
    }

    private void JumpPerformed(InputAction.CallbackContext context)
    {
        isJump = context.ReadValueAsButton();
    }

    private void HandleMoveAction(InputAction.CallbackContext context)
    {
        Horizontal = context.ReadValue<Vector2>().x;
        isMovingPressed = Horizontal !=0;
    }
}
