using System.Linq;
using Concretes.Combats;
using Concretes.States.EnemyStates;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class WormController : MonoBehaviour, IEnemyController
{
    [SerializeField] private HealthSO _healthSo;
    [SerializeField] private AttackSO _attackSo;
    public HealthSO HealthSo => _healthSo;
    public AttackSO AttackSo => _attackSo;
    public Transform Transform { get; private set; }
    public IAttack Attacker { get; set; }
    public IHealth Health { get; private set; }
    public IMoveDal MoveManager { get; private set; }
    public IAnimation Animation { get; private set; }
    public Transform Target { get; set; }
    public float Magnitude { get; }
    public LayerMask EnemyLayerMask => 3;
    public Dead Dead { get; private set; }
    public bool IsFacingRight { get; private set; }

    [Inject] private EnemyManager _enemyManager;

    private StateMachine _stateMachine;
    private NavMeshAgent _navMeshAgent;
    public bool CanAttack { get; private set; }

    private void Awake()
    {
        Transform = transform;
        _navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        Dead = GetComponent<Dead>();
        _stateMachine = new StateMachine();

        Animation = new AnimationController(this);
        MoveManager = new MoveWithNavMesh(this);

        Health = new Health(HealthSo);
        Attacker = AttackSo.GetTypeOfEnemyAttack(this);
    }

    private void Start()
    {
        //FindNearestTarget();
        InitializeStateMachine();
        _enemyManager.AddEnemyToList(this);
        
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        IsFacingRight = true;
    }

    private void Update()
    {
        Dead.IsDead += () =>
        {
            return;
        };
        UpdateSpriteOrientation();
        FindClosestEnemyOnLayer(EnemyLayerMask);
        _stateMachine.Tick();
        IsFacingRight = Transform.GetChild(0).localScale.x > 0;
        if (Target != null)
        {
            CanAttack =
                Vector3.Distance(Target.position, transform.position) <= _navMeshAgent.stoppingDistance 
                && !Target.GetComponent<IPlayerController>().Health.IsDead;
        }
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedTick();
    }

    private void LateUpdate()
    {
        _stateMachine.LateTick();
    }

    private void OnDestroy()
    {
        _enemyManager.RemoveEnemyFromList(this);
    }

    private void InitializeStateMachine()
    {
        //if(Target == null) return;

        IdleState idleState = new IdleState(this);
        ChaseState chaseState = new ChaseState(this);
        AttackState attackState = new AttackState(this);
        DeadState deadState = new DeadState(this);

        _stateMachine.AddState(idleState, chaseState, ()=> Target != null);
        _stateMachine.AddState(chaseState, attackState, () => CanAttack);
        _stateMachine.AddState(attackState, chaseState, () => !CanAttack);
        _stateMachine.AddAnyState(idleState, () => Target == null);
        _stateMachine.AddAnyState(deadState, () => Health.IsDead);

        _stateMachine.SetState(idleState);
    }

    private void UpdateSpriteOrientation()
    {
        Dead.IsDead += () =>
        {
            return;
        };
        if (Target != null)
        {
            Vector2 directionToPlayer = Target.position - Transform.position;

            if (directionToPlayer.x < 0)
            {
                Transform.GetChild(0).localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (directionToPlayer.x > 0)
            {
                Transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }

    public void FindNearestTarget()
    {
        if(Target == null) return;
        
        var nearestTarget = _enemyManager.Targets[0];

        foreach (var target in _enemyManager.Targets)
        {
            var nearestValue = Vector3.Distance(nearestTarget.transform.position, Transform.position);
            var newValue = Vector3.Distance(target.transform.position, Transform.position);

            if (newValue < nearestValue)
            {
                nearestTarget = target;
            }
        }

        Target = nearestTarget.transform;
    }

    public void FindClosestEnemyOnLayer(int layer)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(Transform.position, _attackSo.AttackRange, 0, 1 << layer);
        Target = colliders.FirstOrDefault()?.transform;
    }
}

