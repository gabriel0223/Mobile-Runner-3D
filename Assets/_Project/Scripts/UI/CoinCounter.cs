using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    private int _coinCounter;
    private TextMeshProUGUI _coinTextCounter;

    private void Awake()
    {
        _coinTextCounter = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        Coin.OnCollectCoin += IncreaseCoinCounter;
    }

    private void OnDisable()
    {
        Coin.OnCollectCoin -= IncreaseCoinCounter;
    }

    private void IncreaseCoinCounter(int coinIncrease)
    {
        _coinCounter += coinIncrease;
        _coinTextCounter.SetText(_coinCounter.ToString());
    }
}
