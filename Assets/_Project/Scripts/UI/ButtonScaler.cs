using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonScaler : MonoBehaviour
{
    [SerializeField] private float _scaleMultiplier;
    [SerializeField] private float _animDuration;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(_scaleMultiplier, _animDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
