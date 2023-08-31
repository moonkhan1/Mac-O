using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using Zenject;

public class ElderController : MonoBehaviour, ITalkableNPC,IMissionGiverNPC
{
    public Dialogue Dialogue { get; private set; }
    public event Action OnPlayerApproached;
    private MMPathMovement _mmPathMovement;
    private Collider2D _collider2D;
    [SerializeField] private MissionSO[] killingMissions; 
    [Inject] public MissionManager MissionManager { get; }
    public MissionSO[] MissionSo => killingMissions;
    private void Awake()
    {
        Dialogue = GetComponentInChildren<Dialogue>();
        _mmPathMovement = GetComponent<MMPathMovement>();
        _collider2D = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            OnPlayerApproached?.Invoke();
            _mmPathMovement.MovementSpeed = 0f;
            MMMaths.LookAt2D(col.transform.position);
            _collider2D.enabled = false;
            GiveWormMission();
        }
    }
    private void GiveWormMission()
    {
        MissionManager.StartMission(killingMissions);
    }

}
