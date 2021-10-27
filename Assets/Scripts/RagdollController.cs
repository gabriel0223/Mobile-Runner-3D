using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private PlayerController _playerController;
    
    [SerializeField] private GameObject _ragdollModel;
    [SerializeField] private GameObject _regularModel;
    
    public GameObject RagdollModel => _ragdollModel;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        _playerController.OnDie += ActivateRagdoll;
    }

    private void OnDisable()
    {
        _playerController.OnDie -= ActivateRagdoll;
    }

    private void ActivateRagdoll()
    {
        _ragdollModel.SetActive(true);

        _regularModel.SetActive(false);
    }
}
