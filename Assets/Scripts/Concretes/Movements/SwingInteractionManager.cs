using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwingInteractionManager : IInteractionService
{
    private readonly IPlayerController _playerController;
    private readonly InteractionBase _interactionBase;
    
    LayerMask swingableObject = 7;
    private bool isSwinging = false;
    private Transform currentInteractedObject;
    private Collider2D[] colliderList;

    public SwingInteractionManager(IPlayerController playerController, InteractionBase interactionBase)
    {
        _playerController = playerController;
        _interactionBase = interactionBase;
    }
    public void Tick()
    {
        colliderList = Physics2D.OverlapBoxAll(_playerController.transform.position, new Vector2(5, 5), 1); // Changed the third parameter to 0
        Collider2D isInRangeObject = colliderList.FirstOrDefault(c => c.gameObject.layer == swingableObject);
        if (_playerController.InputReader.isInteraction && !_interactionBase.IsHolding)
        {
            if (!isSwinging && isInRangeObject != null)
            {
                SpringJoint2D springJoint = _playerController.transform.gameObject.AddComponent<SpringJoint2D>();
                LineRenderer lineRenderer = _playerController.transform.GetComponent<LineRenderer>();

                if (lineRenderer == null)
                {
                    lineRenderer = _playerController.transform.gameObject.AddComponent<LineRenderer>();
                }

                springJoint.connectedBody = isInRangeObject.attachedRigidbody;
                springJoint.autoConfigureDistance = false;
                springJoint.distance = 3;
                springJoint.dampingRatio = 0;
                springJoint.frequency = 0;
                isSwinging = true;
                lineRenderer.positionCount = 2;
                lineRenderer.endColor = Color.gray;
                lineRenderer.startColor = Color.gray;
                lineRenderer.startWidth = 0.08f;
                lineRenderer.endWidth = 0.08f;

                lineRenderer.SetPosition(0, _playerController.transform.position);
                lineRenderer.SetPosition(1, isInRangeObject.transform.position);
            }
        }
    }

    public void FixedTick()
    {
        SpringJoint2D springJoint = _playerController.transform.gameObject.GetComponent<SpringJoint2D>();
        LineRenderer lineRenderer = _playerController.transform.GetComponent<LineRenderer>();
        
        springJoint.autoConfigureDistance = false;
        springJoint.distance = 3;
        springJoint.dampingRatio = 0;
        springJoint.frequency = 0;
        isSwinging = true;
        lineRenderer.positionCount = 2;
        lineRenderer.endColor = Color.gray;
        lineRenderer.startColor = Color.gray;
        lineRenderer.startWidth = 0.08f;
        lineRenderer.endWidth = 0.08f;
    }

    public void LateTick()
    {
        if (_playerController.InputReader.isInteraction && !_interactionBase.IsHolding)
        {
            if (isSwinging)
            {
                SpringJoint2D springJoint = _playerController.transform.GetComponent<SpringJoint2D>();
                LineRenderer lineRenderer = _playerController.transform.gameObject.GetComponent<LineRenderer>();
                if (springJoint != null)
                {
                    Object.Destroy(springJoint);
                    lineRenderer.positionCount = 0;
                }

                isSwinging = false;
            }
        }
    }
}
