using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

public class PlayerOneController : MonoBehaviour, IPlayerController
{
    [SerializeField] private HealthSO _healthSo;
    [SerializeField] private AttackSO _attackSo;
    [SerializeField] private MovementSO _movementSo;
    private Transform _transform;
    public HealthSO HealthSo => _healthSo;
    public AttackSO AttackSo => _attackSo;
    public MovementSO MovementSo => _movementSo;
    public GameObject Hook => _hook;
    public ParticleSystem MovementEffect => movementEffect;
    public ParticleSystem JumpEffect => jumpEffect;
    public IInputReader InputReader { get; set; }
    public IJumpService JumpManager {get; private set;}
    public IMovementsService MovementManager {get; private set;}
    public float Speed =>  MovementSo.Speed;
    public float JumpForce => MovementSo.JumpForce;
    public float DashSpeed => MovementSo.DashSpeed;
    public int Damage => AttackSo.Damage;
    public int DashCooldown => MovementSo.DashCooldown;
    public int DashDuration => MovementSo.DashDuration;

    [SerializeField] Rigidbody2D _rigidBody2D;
    [SerializeField] private Transform _interactionIcon;
    [SerializeField] private Transform _rayCastPoint;
    [SerializeField] private Transform _playerWeaponInHand;
    [SerializeField] private GameObject _playerWeapon;
    [SerializeField] private GameObject _hook;
    [SerializeField] private ParticleSystem movementEffect;
    [SerializeField] private ParticleSystem jumpEffect;
    private TrailRenderer _trailRenderer;
    public Transform InteractionUI => _interactionIcon;
    public Transform RayCastPoint => _rayCastPoint;
    public Transform PlayerWeaponInHand => _playerWeaponInHand;
    public GameObject PlayerWeapon => _playerWeapon;
    public Transform ObjectInHand { get; set; }
    public bool CanDash { get; set; } = true;
    public Transform IsSwinging { get; set; }
    public bool Jumping { get; set; } = false;
    public bool IsPlayerAttacking { get; set; } = false;
    public bool IsFacingRight { get; private set; }
    [Inject] public MissionManager MissionManager { get; }
    public bool CanInteractWithObject { get; set; }

    private IFlip _flip;
    private IAnimation _animation;
    private InteractionBase _interactionBase;
    private IDash _dash;
    public IHealth Health { get; private set; }
    public IAttack Attacker { get; private set; }
    public IPlayerMission PlayerMission { get; private set; }
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _trailRenderer = GetComponent<TrailRenderer>();
        InputReader = new InputOneReader();
        MovementManager = new PlayerMovementManager(this, new MoveWithTransformMethod(_transform));
        JumpManager = new PlayerJumpManager(this, new JumpWithDifferentMethods(_rigidBody2D));
        _flip = new FlipWIthDifferentMethods(this);
        _animation = new AnimationController(this);
        _interactionBase = new InteractionController(this);
        _dash = new PlayerDash(this);
        Attacker = new RangeAttack(this);
        Health = new Health(HealthSo);
        PlayerMission = new PlayerMissionManager(this);

    }

    private void OnEnable()
    {
        Health.OnDead += DeadActionAsync;
    }
    
    private async void DeadActionAsync()
    {
        const int delayTimeBeforeDestroy = 1000;
        _animation.DeadAnimation("IsDead");
        
        await Task.Delay(delayTimeBeforeDestroy);
        if(this == null) return;
        Destroy(this.gameObject);
        SceneLoadManager.Instance.LoadGameOnDead();
    }

    private void Start()
    {
        IsFacingRight = true;
    }

    private void OnValidate() 
    {
        GetReference();
    }
    private void Update() 
    {
        if(Health.IsDead) return;
        MovementManager.Tick();
        JumpManager.Tick();
        _interactionBase.Interact(_interactionIcon, _rayCastPoint);
        _flip.FlipAction();
        _interactionBase.SwingInteract();
        _dash.DashAction();
        Attacker.AttackAction(Attacker);
        PlayerMission.Tick();
        IsFacingRight = _transform.GetChild(0).localScale.x > 0;
    }

    private void FixedUpdate() 
    {
        if(Health.IsDead) return;
        MovementManager.FixedTick();
        JumpManager.FixedTick();
    }

    private void LateUpdate()
    {
        if(Health.IsDead) return;
        _animation.RunAnimation(InputReader.isMovingPressed && !ObjectInHand && !IsSwinging);
        _animation.JumpAnimation(Jumping);
        _animation.PullOrPushAnimation(ObjectInHand, InputReader.Horizontal);
        _animation.DashAnimation(DashDuration ,InputReader.isDash, CanDash);
        _animation.AttackAnimation(IsPlayerAttacking);
        _animation.SwingAnimation(IsSwinging);
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
