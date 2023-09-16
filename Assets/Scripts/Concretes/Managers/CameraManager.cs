using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public string TagName;
    [SerializeField] private CinemachineVirtualCamera _firstCamera;
    [SerializeField] private CinemachineVirtualCamera[] _allCameras;

    private void Start()
    {
        ChangeCamera(_firstCamera);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagName))
        {
            Debug.Log("camera");
            CinemachineVirtualCamera targetCamera = col.GetComponentInChildren<CinemachineVirtualCamera>();
            ChangeCamera(targetCamera);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(TagName))
        {
            ChangeCamera(_firstCamera);
        }
    }
    private void ChangeCamera(CinemachineVirtualCamera targetCamera)
    {
        foreach (CinemachineVirtualCamera camera in _allCameras)
        {
            camera.enabled = camera == targetCamera;
        }
    }

    [ContextMenu("Get All Virtual Cameras")]
    private void GetAllCameras()
    {
        _allCameras = FindObjectsByType<CinemachineVirtualCamera>(FindObjectsSortMode.None);
    }
}
