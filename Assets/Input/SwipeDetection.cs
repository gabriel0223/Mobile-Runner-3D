using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float minimumDistance;
    [SerializeField] private float maximumTime;
    [SerializeField, Range(0, 1)] private float directionThreshold;
    
    private InputManager _inputManager;
    private Vector2 _startPosition;
    private float _startTime;
    private Vector2 _endPosition;
    private float _endTime;
    
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    
    public event Action OnSwipeUp;
    public event Action OnSwipeDown;

    public delegate void SwipeSide(int dir);
    public event SwipeSide OnSwipeSide;

    private void Awake()
    {
        _inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        _inputManager.OnStartTouch += SwipeStart;
        _inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        _inputManager.OnStartTouch -= SwipeStart;
        _inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        _startPosition = position;
        _startTime = time;
    }
    
    private void SwipeEnd(Vector2 position, float time)
    {
        _endPosition = position;
        _endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        float touchDistance = Vector3.Distance(_startPosition, _endPosition);
        float touchDuration = _endTime - _startTime;
        
        if (touchDistance >= minimumDistance && touchDuration < maximumTime)
        {
            Debug.DrawLine(_startPosition, _endPosition, Color.red, 5f);
            Vector3 direction = _endPosition - _startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        List<float> dotDirections = new List<float>
        {
            Vector2.Dot(Vector2.up, direction),
            Vector2.Dot(Vector2.down, direction),
            Vector2.Dot(-Vector2.left, direction),
            Vector2.Dot(-Vector2.right, direction)
        };

        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            OnSwipeUp?.Invoke();
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            OnSwipeDown?.Invoke();
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            OnSwipeSide?.Invoke(-1);
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            OnSwipeSide?.Invoke(1);
        }
    }
}
