using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Coin : Collectable
{
    [SerializeField] private int _coinValue;

    public static event Action<int> OnCollectCoin; 

    public override void Collect()
    {
        base.Collect();
        OnCollectCoin?.Invoke(_coinValue);
    }
}
