using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpWithDifferentMethods : IJumpDal
{
    private readonly Rigidbody2D _rb2D;
    public JumpWithDifferentMethods(Rigidbody2D rb2D)
    {
        _rb2D = rb2D;
    }
    public void JumpAction(float value)
    {
        float JumpForceValue = value * Time.deltaTime;
        _rb2D.AddForce(Vector3.up * JumpForceValue, ForceMode2D.Impulse);
    }
}
