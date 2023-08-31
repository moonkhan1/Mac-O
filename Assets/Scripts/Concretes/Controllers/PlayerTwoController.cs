using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTwoController : MonoBehaviour, IPlayerController
{
    private Transform _transform;
    public IInputReader InputReader { get; set; }
    public IJumpService JumpManager {get; private set;}
    public IMovementsService MovementManager {get; private set;}
    public IHealth Health { get; }
    public IAttack Attacker { get; }
    public IPlayerMission PlayerMission { get; }
    public float Speed { get; } = 15f;
    public float JumpForce { get; } = 15000f;
    public float DashSpeed { get; } = 25f;
    public int Damage { get; }
    public int DashCooldown { get; }
    public int DashDuration { get; }

    [SerializeField] Rigidbody2D _rigidBody2D;
    [SerializeField] private Transform _interactionIcon;
    [SerializeField] private Transform _rayCastPoint;
    [SerializeField] private Transform _playerWeaponInHand;
    [SerializeField] private GameObject _playerWeapon;
    private Transform _objectInHand;
    public Transform ObjectInHand { get; set; }
    public bool CanInteractWithObject { get; set; }
    public bool CanDash { get; set; }
    public Transform IsSwinging { get; set; }
    public bool Jumping { get; set; }
    public bool IsPlayerAttacking { get; set; }
    public bool IsFacingRight { get; set; }
    public HealthSO HealthSo { get; }
    public AttackSO AttackSo { get; }
    public MovementSO MovementSo { get; }
    public GameObject Hook { get; set; }
    public Animator LandingEffect { get; }
    public ParticleSystem MovementEffect { get; }
    public ParticleSystem JumpEffect { get; }
    public List<MissionSO> Missions { get; }
    public KilledEnemyTypes _killedEnemyTypes { get; set; }
    public MissionManager MissionManager { get; }
    public List<MissionSO> MissionSOs { get; }
    public Transform InteractionUI => _interactionIcon;
    public Transform RayCastPoint => _rayCastPoint;
    public Transform PlayerWeaponInHand => _playerWeaponInHand;
    public GameObject PlayerWeapon => _playerWeapon;
    private IFlip _flip;
    private IAnimation _animation;
    private InteractionBase _interactionBase;


    private void Awake()
    {
        _transform = GetComponent<Transform>();
        InputReader = new InputTwoReader();
        MovementManager = new PlayerMovementManager(this, new MoveWithRigidbodyDal(_rigidBody2D));
        JumpManager = new PlayerJumpManager(this, new JumpWithDifferentMethods(_rigidBody2D));
        _flip = new FlipWIthDifferentMethods(this);
        _animation = new AnimationController(this);
        _interactionBase = new InteractionController(this);
    }
    void OnValidate() 
    {
        GetReference();
    }
    void Update() 
    {
        _flip.FlipAction();
        MovementManager.Tick();
        JumpManager.Tick();
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        MovementManager.FixedTick();
        JumpManager.FixedTick();
        _interactionBase.Interact(_interactionIcon, _rayCastPoint);
    }

    private void LateUpdate()
    {
        _animation.RunAnimation(InputReader.isMovingPressed);
        _animation.JumpAnimation(InputReader.isJump);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        JumpManager.ResetJumpCounter();
    }
    private void GetReference()
    {
        if(_rigidBody2D == null)
            _rigidBody2D = GetComponent<Rigidbody2D>();
    }
}
