using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerDash : IDash
{
    readonly IPlayerController _playerController;
    private readonly Rigidbody2D _rb2D;

    public PlayerDash(IPlayerController playerController)
    {
        _playerController = playerController;
        _rb2D = _playerController.transform.GetComponent<Rigidbody2D>();
    }

    public async void DashAction()
    {
        bool isDashPressed = _playerController.InputReader.isDash;
        if (isDashPressed && _playerController.CanDash)
        {
            await Dash();
        }
    }
    private async Task Dash()
    {
        _playerController.CanDash = false;
        float originalGravity = _rb2D.gravityScale;
        float horizontalInput = _playerController.InputReader.Horizontal;
        float dashSpeed = _playerController.DashSpeed;
        int dashCooldown = _playerController.DashCooldown;
        int dashDuration = _playerController.DashDuration;
        var trailRenderer = _playerController.transform.GetComponent<TrailRenderer>();
        if (_playerController.ObjectInHand == null)
        {
            _rb2D.gravityScale = 0f;
            _rb2D.velocity = (new Vector2(horizontalInput * dashSpeed, _rb2D.velocity.y));
            trailRenderer.emitting = true;
            await Task.Delay(dashDuration);
            _rb2D.velocity = (new Vector2(0f, _rb2D.velocity.y));
            trailRenderer.emitting = false;
            _rb2D.gravityScale = originalGravity;
            await Task.Delay(dashCooldown); 
            _playerController.CanDash = true;
        }

    }


}
