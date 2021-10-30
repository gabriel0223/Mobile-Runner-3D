using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplayer : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private TextMeshProUGUI _bestScore;
    private void OnEnable()
    {
        _currentScore.SetText(_player.GetDistanceTraveled() + "m");
        _bestScore.SetText(PlayerPrefs.GetInt(SaveKeys.BEST_SCORE) + "m");
    }
}
