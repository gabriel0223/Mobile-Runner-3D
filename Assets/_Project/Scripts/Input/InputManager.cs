using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    
    private PlayerControls _playerControls;
    private Camera _mainCamera;
    
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;

    private void Awake()
    {
        Instance = this;
        
        _playerControls = new PlayerControls();
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Start()
    {
        _playerControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        _playerControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        OnStartTouch?.Invoke(PrimaryPosition(), (float)ctx.startTime);
    }
    
    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        OnEndTouch?.Invoke(PrimaryPosition(), (float)ctx.startTime);
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(_mainCamera, _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
