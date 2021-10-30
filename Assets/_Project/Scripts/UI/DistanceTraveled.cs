using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceTraveled : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    
    private TextMeshProUGUI _distanceText;

    private void Awake()
    {
        _distanceText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _distanceText.SetText(player.GetDistanceTraveled() + "m");
    }
}
