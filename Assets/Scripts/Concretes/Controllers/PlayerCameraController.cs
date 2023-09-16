using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    private IPlayerController _playerController;
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private void Awake()
    {
        _playerController = _playerTransform.gameObject.GetComponent<IPlayerController>();
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void LateUpdate()
    {
        CinemachineFramingTransposer cinemachineFramingTransposer;
        cinemachineFramingTransposer =
            _cinemachineVirtualCamera.transform.GetComponentInChildren<CinemachineFramingTransposer>();
        DOTween.To(() => cinemachineFramingTransposer.m_TrackedObjectOffset.x,
            x => cinemachineFramingTransposer.m_TrackedObjectOffset.x = x, DetermineEndRotation(2), 0.8f);
        // cinemachineFramingTransposer.m_TrackedObjectOffset.x = DetermineEndRotation(3);
    }

    private int DetermineEndRotation(int value)
    {
        if (_playerController.IsFacingRight)
        {
            return value;
        }

        return -value;
    }
}
