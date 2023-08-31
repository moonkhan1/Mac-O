using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithTransformMethod : IMoveDal
{
    readonly Transform _transform;
    // private readonly Rigidbody2D _rb2D;
    public MoveWithTransformMethod(Transform transform)
    {
        _transform = transform;
        // _rb2D = _transform.GetComponent<Rigidbody2D>();
    }

    public void MoveAction(Vector2 direction, float value)
    {
        _transform.Translate(value * direction);
        // Vector2 movement = new Vector2(value, 0f);
        // _rb2D.velocity = movement;
    }
    
}

public class MoveWithRigidbodyDal : IMoveDal
{
    readonly Rigidbody2D _rigidBody2D;

    public MoveWithRigidbodyDal(Rigidbody2D rigidBody2D)
    {
        _rigidBody2D = rigidBody2D;
    }

    public void MoveAction(Vector2 direction, float value)
    {
        _rigidBody2D.MovePosition(direction * value);
    }
}