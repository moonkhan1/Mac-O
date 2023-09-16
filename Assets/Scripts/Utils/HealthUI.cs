using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> _healthSprites;
    private IPlayerController _playerController;
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");

    private void Awake()
    {
        _playerController = GetComponentInParent<IPlayerController>();
    }

    private void Start()
    {
        _playerController.Health.OnTakeDamage += DestroyOneHealthCrystal;
        _playerController.Health.OnDead += DestroyAllHealthCrystal;
    }

    private void OnDisable()
    {
        _playerController.Health.OnTakeDamage -= DestroyOneHealthCrystal;
        _playerController.Health.OnDead -= DestroyAllHealthCrystal;
    }

    private void DestroyOneHealthCrystal()
    {
        int index = 0;
        var healthUI = _healthSprites[index];
        _healthSprites.Remove(healthUI);
        healthUI.SetActive(false);
    }

    private void DestroyAllHealthCrystal()
    {
        if(_playerController.Health.IsDead)
            _healthSprites.ForEach(h => h.SetActive(false));
    }
    
}
