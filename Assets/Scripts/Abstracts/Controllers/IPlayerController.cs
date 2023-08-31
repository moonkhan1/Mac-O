using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IPlayerController : IEntityController
{
    IInputReader InputReader { get; }
    IJumpService JumpManager { get; }
    IMovementsService MovementManager { get; }
    IHealth Health { get; }
    IAttack Attacker {get;}
    IPlayerMission PlayerMission { get; }
    float Speed { get; }
    float JumpForce { get; }
    float DashSpeed { get; }
    int Damage { get; }
    int DashCooldown { get; }
    int DashDuration { get; }
    Transform InteractionUI { get; }
    Transform RayCastPoint { get; }
    Transform PlayerWeaponInHand { get; }
    GameObject PlayerWeapon { get; }
    Transform ObjectInHand { get; set; }
    bool CanInteractWithObject { get; set;}
    bool CanDash { get; set; }
    Transform IsSwinging { get; set;}
    bool Jumping { get; set; }
    bool IsPlayerAttacking { set; }
    //bool IsFacingRight { get; }
    HealthSO HealthSo { get; }
    AttackSO AttackSo { get; }
    MovementSO MovementSo { get; }
    GameObject Hook { get;}
    // Animator LandingEffect { get; }
    ParticleSystem MovementEffect { get; }
    ParticleSystem JumpEffect { get; }
    MissionManager MissionManager { get; } 
}