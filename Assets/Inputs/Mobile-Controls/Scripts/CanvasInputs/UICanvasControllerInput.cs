using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TwoD.Platformer;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class UICanvasControllerInput : MonoBehaviour
{
    [Header("Output")]
    [Inject]
    private InputOneReader inputs;

    int _inventoryIndex;
    int _jumpIndex;

    private void OnEnable()
    {
        inputs.Input.Player.Jump.started += VirtualJumpInput;
        inputs.Input.Player.Jump.canceled += VirtualJumpInput;
        
        inputs.Input.Player.Interaction.started += VirtualInteractionInput;
        inputs.Input.Player.Interaction.performed += VirtualInteractionInput;
        inputs.Input.Player.Interaction.canceled += VirtualInteractionInput;
        
        inputs.Input.Player.Dash.started += VirtualSprintInput;
        inputs.Input.Player.Dash.performed += VirtualSprintInput;
        inputs.Input.Player.Dash.canceled += VirtualSprintInput;
    }

    private void OnDisable()
    {
        inputs.Input.Player.Jump.started -= VirtualJumpInput;
        inputs.Input.Player.Jump.canceled -= VirtualJumpInput;
        
        inputs.Input.Player.Interaction.started -= VirtualInteractionInput;
        inputs.Input.Player.Interaction.performed -= VirtualInteractionInput;
        inputs.Input.Player.Interaction.canceled -= VirtualInteractionInput;
        
        inputs.Input.Player.Dash.started -= VirtualSprintInput;
        inputs.Input.Player.Dash.performed -= VirtualSprintInput;
        inputs.Input.Player.Dash.canceled -= VirtualSprintInput;
    }
    
    public async void VirtualJumpInput(InputAction.CallbackContext callbackContext)
    {
        // inputs.isJump = true;
        // await WaitFrameForWeaponJumpAsync();
        if (inputs.isJump && callbackContext.action.triggered) return;
        inputs.isJump = callbackContext.ReadValueAsButton();

        await WaitFrameForWeaponJumpAsync();
    }
    
    public void VirtualSprintInput(InputAction.CallbackContext callbackContext)
    {
        inputs.isDash = inputs.Input.Player.Dash.IsPressed();
    }

    private async void VirtualInteractionInput(InputAction.CallbackContext callbackContext)
    {
        if (inputs.isInteraction && inputs.Input.Player.Interaction.triggered) return;
        inputs.isInteraction = inputs.Input.Player.Interaction.IsPressed();
    
        await WaitFrameForWeaponInteractionAsync();
    }
    private async Task WaitFrameForWeaponInteractionAsync()
    {
        inputs.isInteraction = true && _inventoryIndex % 2 == 0;
        await UniTask.Yield(); 
        inputs.isInteraction = false;
        _inventoryIndex++;
    }
    private async Task WaitFrameForWeaponJumpAsync()
    {
        inputs.isJump = true && _jumpIndex % 2 == 0;
        await UniTask.Yield(); 
        inputs.isJump = false;
        _jumpIndex++;
    }

}