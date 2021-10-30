using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PopUpWindow : MonoBehaviour
{
    [SerializeField] private float _animationDuration;
    private void OnEnable()
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(originalScale, _animationDuration);
    }
}
