using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipWIthDifferentMethods : IFlip
{
    readonly IPlayerController _playerController;
    readonly Transform _transform;

    public FlipWIthDifferentMethods(IPlayerController playerController)
    {
        _playerController = playerController;
        _transform = _playerController.transform.GetChild(0).transform;
    }

    public void FlipAction()
    {
        if(_playerController.ObjectInHand == null)
            FlipWithLocalScale();
    }

    private void FlipWithLocalScale()
    {
        float horizontalInput = _playerController.InputReader.Horizontal;
        var interactionUI = _playerController.InteractionUI.GetComponent<SpriteRenderer>();
        if(horizontalInput == 0f) return;
        
        if (horizontalInput < 0f)
        {
            horizontalInput = -1f;
            _transform.localScale = new Vector3(horizontalInput,1f,1f);
            interactionUI.flipX = true;
        }
        if (horizontalInput > 0f)
        {
            horizontalInput = 1f;
            _transform.localScale = new Vector3(horizontalInput,1f,1f);
            interactionUI.flipX = false;
        }
    }
    
    private void FlipWithXY()
    {
        float horizontalInput = _playerController.InputReader.Horizontal;
        var spriteRenderer = _transform.GetComponent<SpriteRenderer>();
        var playerRayCastPoint = _playerController.RayCastPoint;
        
        if (horizontalInput == 0f) return;
        if (horizontalInput > 0f)
        {
            spriteRenderer.flipX = false;
            Vector3 tempLocalScale = playerRayCastPoint.localScale;
            tempLocalScale.x = 1f;
            playerRayCastPoint.localScale = tempLocalScale;

        }
        else if (horizontalInput < 0f)
        {
            spriteRenderer.flipX = true;
            var tempLocalScale = playerRayCastPoint.localScale;
            tempLocalScale.x = -1f;
            playerRayCastPoint.localScale = tempLocalScale;
        }
    }
}
