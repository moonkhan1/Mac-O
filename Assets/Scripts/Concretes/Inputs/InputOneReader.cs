using System;
using System.Collections;
using System.Threading.Tasks;
using TwoD.Platformer;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputOneReader : IInputReader
{
    readonly PlayerOneInput _input;
    public float Horizontal { get; private set; }
    public bool isJump { get;  private set; }
    public bool isMovingPressed { get;  private set; }
    public bool isInteraction { get; private set; }
    public bool isDash { get; private set; }
    
    int _inventoryIndex;
    int _jumpIndex;
    public InputOneReader()
    {
        _input = new PlayerOneInput();
        _input.Enable();
        _input.Player.Move.performed += HandleMoveAction;
        _input.Player.Move.canceled += HandleMoveAction;
        _input.Player.Jump.started += JumpPerformed;
        //_input.Player.Jump.performed += JumpPerformed;
        _input.Player.Jump.canceled += JumpPerformed;
        _input.Player.Interaction.started += InteractionPerformed;
        _input.Player.Interaction.performed += InteractionPerformed;
        _input.Player.Interaction.canceled += InteractionPerformed;
        _input.Player.Dash.started += DashPerformed;
        _input.Player.Dash.performed += DashPerformed;
        _input.Player.Dash.canceled += DashPerformed;
    }

    private void OnDisable()
    {
        _input.Player.Move.performed -= HandleMoveAction;
        _input.Player.Move.canceled -= HandleMoveAction;
        _input.Player.Jump.started -= JumpPerformed;
        //_input.Player.Jump.performed -= JumpPerformed;
        _input.Player.Jump.canceled -= JumpPerformed;
        _input.Player.Interaction.started -= InteractionPerformed;
        _input.Player.Interaction.performed -= InteractionPerformed;
        _input.Player.Interaction.canceled -= InteractionPerformed;
        _input.Player.Dash.started -= DashPerformed;
        _input.Player.Dash.performed -= DashPerformed;
        _input.Player.Dash.canceled -= DashPerformed;
        _input.Dispose();
    }

    private void DashPerformed(InputAction.CallbackContext context)
    {
        isDash = context.ReadValueAsButton();
    }

    private async void InteractionPerformed(InputAction.CallbackContext context)
    {
        if (isInteraction && context.action.triggered) return;
        isInteraction = context.ReadValueAsButton();

        await WaitFrameForWeaponInteractionAsync();
    }
    private async void JumpPerformed(InputAction.CallbackContext context)
    {
        if (isJump && context.action.triggered) return;
        isJump = context.ReadValueAsButton();

        await WaitFrameForWeaponJumpAsync();
    }
    private void HandleMoveAction(InputAction.CallbackContext context)
    {
        Horizontal = context.ReadValue<Vector2>().x;
        isMovingPressed = Horizontal !=0;
    }
    
    private async Task WaitFrameForWeaponInteractionAsync()
    {
        isInteraction = true && _inventoryIndex % 2 == 0;
        await Task.Yield(); 
        isInteraction = false;
        _inventoryIndex++;
    }
    private async Task WaitFrameForWeaponJumpAsync()
    {
        isJump = true && _jumpIndex % 2 == 0;
        await Task.Yield(); 
        isJump = false;
        _jumpIndex++;
    }
}
