using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _playerCamera;
    [SerializeField] private GameObject _ragdollCamera;

    private void OnEnable()
    {
        _playerController.OnDie += FollowRagdoll;
    }
    
    private void OnDisable()
    {
        _playerController.OnDie -= FollowRagdoll;
    }

    private void FollowRagdoll()
    {
        _playerCamera.SetActive(false);
        _ragdollCamera.SetActive(true);
    }
}
