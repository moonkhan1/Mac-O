using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using MoreMountains.Tools;
using UnityEngine;

public class InteractionController : InteractionBase
{
    private IPlayerController _playerController;
    private bool isHolding = false;
    private bool isSwinging = false;
    private Collider2D[] colliderList;
    private LineRenderer _lineRenderer;
    private SpringJoint2D _springJoint;
    private Vector3 endPos;
    public InteractionController(IPlayerController playerController)
    {
        _playerController = playerController;
        _lineRenderer = _playerController.transform.gameObject.GetComponent<LineRenderer>();
    }

    public override void Interact(Transform interactionUI, Transform rayCastPoint)
    {
        LayerMask objectInteraction = 8;
        float maxHitDistance = 1f;
        Vector2 rayDirection = _playerController.IsFacingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit2D = Physics2D.Raycast(rayCastPoint.position, rayDirection, maxHitDistance);
    
        bool canInteract = false;

        if (hit2D.collider != null)
        {
            canInteract = hit2D.transform.gameObject.layer == objectInteraction;
            interactionUI.gameObject.SetActive(canInteract);
        }
        else
        {
            interactionUI.gameObject.SetActive(false);
            if(_playerController.ObjectInHand == null) return;
            _playerController.ObjectInHand.SetParent(null);
            _playerController.ObjectInHand = null;
            isHolding = false;
        }

        _playerController.CanInteractWithObject = canInteract;

        if (canInteract && _playerController.InputReader.isInteraction && !isSwinging)
        {
            Transform objectInHand = _playerController.ObjectInHand;

            if (isHolding)
            {
                if (objectInHand == hit2D.transform)
                {
                    // Release the object
                    objectInHand.SetParent(null);
                    _playerController.ObjectInHand = null;
                    isHolding = false;
                }
            }
            else if (objectInHand == null)
            {
                // Pick up the object
                hit2D.transform.SetParent(_playerController.transform);
                objectInHand = hit2D.transform;
                _playerController.ObjectInHand = objectInHand;
                isHolding = true;
            }

        }
    }
    public override void SwingInteract()
    {
        LayerMask swingableObject = 7;
        if (_playerController.InputReader.isInteraction && !isHolding)
        {
            Collider2D isInRangeObject = FindClosestObjectOnLayer(swingableObject);
        
            if (!isSwinging && isInRangeObject != null)
            {
                InitializeSwing(isInRangeObject);
            }
            else if (isSwinging)
            {
                EndSwing();
            }
        }
    
        UpdateSwingLineRenderer();
    }

    private Collider2D FindClosestObjectOnLayer(int layer)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(_playerController.transform.position, new Vector2(7, 7), 0, 1 << layer);
        return colliders.FirstOrDefault();
    }

    private void InitializeSwing(Collider2D swingableObject)
    {
        _springJoint = _playerController.transform.gameObject.AddComponent<SpringJoint2D>();
        _springJoint.connectedBody = swingableObject.attachedRigidbody;
        _springJoint.autoConfigureDistance = false;
        _springJoint.distance = 3;
        _springJoint.dampingRatio = 1;
        _springJoint.frequency = 0;
        _playerController.IsSwinging = _springJoint.connectedBody.transform;
        isSwinging = true;
        _lineRenderer.positionCount = 2;
    }

    private void EndSwing()
    {
        Destroy(_springJoint);
        _lineRenderer.positionCount = 0;
        _playerController.IsSwinging = null;
        isSwinging = false;
    }

    private void UpdateSwingLineRenderer()
    {
        if (isSwinging)
        {
            Vector3 playerToHook = _springJoint.connectedBody.transform.position - _playerController.transform.position;
            Vector3 swingDirection = playerToHook.normalized;
            
            _lineRenderer.SetPosition(0, _playerController.transform.position);
            _lineRenderer.SetPosition(1, _springJoint.connectedBody.transform.position);
            _playerController.Hook.SetActive(true);
            Vector3 newHookPosition = _lineRenderer.GetPosition(1);
            _playerController.Hook.transform.position = newHookPosition;

            float targetAngle = Mathf.Atan2(swingDirection.y, swingDirection.x) * Mathf.Rad2Deg;

            // Apply the rotation only to the z-axis of the hook
            Vector3 hookEulerAngles = _playerController.Hook.transform.eulerAngles;
            hookEulerAngles.z = targetAngle;
            _playerController.Hook.transform.eulerAngles = hookEulerAngles;
        }
        else
        {
            _playerController.Hook.SetActive(false);
        }
    }
}

