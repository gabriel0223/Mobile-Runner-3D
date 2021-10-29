using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Collectable : MonoBehaviour, ICollectable
{
    [Header("Animation Settings")]
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _sineMovementHeight;
    [SerializeField] private float _sineMovementDuration;
    [Header("Sound Settings")]
    [SerializeField] private AudioCueSO _collectSound;
    
    private AudioCue _audioCue;
    
    private void Awake()
    {
        _audioCue = GetComponent<AudioCue>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        PlaySineAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        RotateHorizontally();
    }

    private void RotateHorizontally()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
    }
    
    private void PlaySineAnimation()
    {
        transform.DOLocalMoveY(_sineMovementHeight, _sineMovementDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    public virtual void Collect()
    {
        transform.DOScale(Vector3.zero, 0.15f).OnComplete(() => Destroy(gameObject));
        _audioCue.PlayAudioCue(_collectSound);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<PlayerController>()) return;

        Collect();
    }
}
