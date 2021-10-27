using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private SwipeDetection _swipeDetection;
    private PlayerAnimation _playerAnim;
    [SerializeField] private Lanes _lanes;
    [SerializeField] private LayerMask groundLayer;
    
    private int _currentLaneIndex = 1;
    private bool switchingLane;

    [Header("Movement Settings")]
    [SerializeField] private float _playerAcceleration;
    [SerializeField] private float _maxSpeed;
    [Tooltip("How fast will the player switch lanes?")]
    [SerializeField] private float _switchLaneSpeed;
    [SerializeField] private float _jumpForce;

    [Header("Progression Settings")]
    [Tooltip("How much will the player max speed increase over time?")]
    [SerializeField] private float _maxSpeedIncrease;

    public event Action OnDie;

    private bool dead;
    public bool Dead => dead;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _swipeDetection = InputManager.Instance.gameObject.GetComponent<SwipeDetection>();
        _playerAnim = GetComponentInChildren<PlayerAnimation>();
    }

    private void OnEnable()
    {
        _swipeDetection.OnSwipeUp += Jump;
        _swipeDetection.OnSwipeDown += Slide;
        _swipeDetection.OnSwipeSide += Move;
    }

    private void OnDisable()
    {
        _swipeDetection.OnSwipeUp -= Jump;
        _swipeDetection.OnSwipeSide -= Move;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) return;
        
        _rb.velocity = new Vector3(_rb.velocity.x, 0, Mathf.Clamp(_rb.velocity.z, 0, _maxSpeed));
        _maxSpeed += _maxSpeedIncrease * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (dead) return;
        
        _rb.velocity += new Vector3(0, 0, _playerAcceleration) * Time.deltaTime;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1, groundLayer);
    }
    
    private void Jump()
    {
        if (!IsGrounded()) return;
        
        _rb.DOMoveY(_jumpForce, 0.4f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutSine);
        _playerAnim.PlayAnimation("Jump", 0.1f);
    }

    private void Slide()
    {
        _playerAnim.PlayAnimation("Slide", 0.1f);
    }

    private void Move(int direction)
    {
        int targetLaneIndex = _currentLaneIndex + direction;
        
        if (dead || switchingLane || targetLaneIndex < 0 || targetLaneIndex >= _lanes.LanesList.Length) return;

        switchingLane = true;

        Vector3 targetLanePosition = _lanes.LanesList[targetLaneIndex].Position;
        _currentLaneIndex += direction > 0 ? 1 : -1;

        _rb.DOMoveX(targetLanePosition.x, 1 / _switchLaneSpeed).OnComplete(() => switchingLane = false);
        _playerAnim.PlayAnimation("Switch_Lane", 0.1f);
    }

    public void Die()
    {
        OnDie?.Invoke();
        dead = true;
        _rb.isKinematic = true;
    }
}
