using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveWithNavMesh : IMoveDal
{
    private NavMeshAgent _navMeshAgent;
    private Quaternion initialRotation;
    private Transform _transform;

    public MoveWithNavMesh(IEntityController entityController)
    {
        _transform = entityController.transform;
        _navMeshAgent = entityController.transform.GetComponentInChildren<NavMeshAgent>();
        initialRotation = _transform.rotation;
    }

    public void MoveAction(Vector2 direction, float value)
    {
        var newDestination = new Vector3(direction.x, _transform.position.y, direction.y);
        _transform.rotation = initialRotation;
        _navMeshAgent.SetDestination(newDestination);
    }
    
}
