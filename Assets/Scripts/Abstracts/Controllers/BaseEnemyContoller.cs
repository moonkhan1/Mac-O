using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemyContoller : MonoBehaviour
{
    protected HealthSO HealthSo;
    protected AttackSO AttackSo;
    public bool IsFacingRight { get; }
    public Transform Transform { get; private set; }
    public IAttack Attacker { get; protected set; }
    public IHealth Health { get; protected set; }
    public IMoveDal MoveManager { get; private set; }
    public IAnimation Animation { get; private set; }
    public Transform Target { get; }
    public float Magnitude { get; }
    public Dead Dead { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    public StateMachine StateMachine { get; protected set; }

    public virtual void FindNearestTarget()
    {
        throw new System.NotImplementedException();
    }

    protected virtual void Awake()
    {
        Transform = transform;
        Attacker = CreateAttacker();
        Health = CreateHealth();
        NavMeshAgent = GetComponentInChildren<NavMeshAgent>();
        Dead = GetComponent<Dead>();
        // Animation = new AnimationController(this);
        // MoveManager = new MoveWithNavMesh(this);
        StateMachine = new StateMachine();
    }
    
    protected virtual void Start()
    {
        NavMeshAgent.updateRotation = false;
        NavMeshAgent.updateUpAxis = false;
        // Shared start logic for all enemy types
    }

    protected virtual void Update()
    {
        // Shared update logic for all enemy types
        StateMachine.Tick();
    }

    protected virtual void FixedUpdate()
    {
        // Shared fixed update logic for all enemy types
        StateMachine.FixedTick();
    }

    protected virtual void LateUpdate()
    {
        // Shared late update logic for all enemy types
        StateMachine.LateTick();
    }

    protected virtual IAttack CreateAttacker()
    {
        // Override this method in derived classes to create specific attackers
        return null;
    }

    protected virtual IHealth CreateHealth()
    {
        // Override this method in derived classes to create specific health
        return null;
    }

    protected virtual void InitializeStateMachine()
    {
        // Shared state machine initialization logic for all enemy types
    }

    
}
