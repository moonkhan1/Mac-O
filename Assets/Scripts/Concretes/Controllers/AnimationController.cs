using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationController : IAnimation
{
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int PullOrPush = Animator.StringToHash("IsPullOrPushing");
    private static readonly int IsDash = Animator.StringToHash("IsDash");
    private static readonly int IsDashing = Animator.StringToHash("IsDashing");
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int IsSwinging = Animator.StringToHash("IsSwinging");
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    private static readonly int IsPullingInMotion = Animator.StringToHash("IsPullingInMotion");
    public Animator _animator { get; }
    private Rigidbody2D _rigidbody2D;


    public AnimationController(IEntityController playerController)
    {
        _animator = playerController.transform.GetComponentInChildren<Animator>();
        _rigidbody2D = playerController.transform.GetComponent<Rigidbody2D>();
    }
    
    public void JumpAnimation(bool isJumping)
    {
        _animator.SetBool(Jump, isJumping);
        _animator.SetFloat(YVelocity, _rigidbody2D.velocity.y);
    }

    public void RunAnimation(bool isRunning)
    {
        _animator.SetBool(IsRunning, isRunning);
    }

    public async void DashAnimation(int dashDuration, bool isDashPressed, bool canDash)
    {
        if (!canDash) return;
        if (!isDashPressed) return;
        _animator.SetBool(IsDash, true);
        await UniTask.Delay(dashDuration);
        _animator.SetBool(IsDash, false);
    }

    public void AttackAnimation(bool isAttacking)
    {
        _animator.SetBool(IsAttack, isAttacking);
    }

    public void DeadAnimation(string isDead)
    {
        _animator.SetTrigger(isDead);
    }

    public void SwingAnimation(bool IsSwiging)
    {
        _animator.SetBool(IsSwinging, IsSwiging);
    }


    public void PullOrPushAnimation(bool isPullOrPushing, float horizontal)
    {
        if (isPullOrPushing)
        {
            _animator.SetBool(PullOrPush, true);
            _animator.SetBool(IsPullingInMotion, horizontal!= 0);
        }
        else
        {
            _animator.SetBool(PullOrPush, false);
            _animator.SetBool(IsPullingInMotion, false);
        }
    }
}
