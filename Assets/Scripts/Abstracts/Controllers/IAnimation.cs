using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimation 
{
    Animator _animator { get; }
    void JumpAnimation(bool isJumping);
    void RunAnimation(bool isRunning);
    void PullOrPushAnimation(bool isPullOrPushing, float Horizontal);
    void DashAnimation(int dashDuration,bool isDashPressed, bool canDash);
    void AttackAnimation(bool IsAttacking);
    void DeadAnimation(string IsDead);
    void SwingAnimation(bool IsSwinging);
}
