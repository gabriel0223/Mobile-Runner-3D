using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameObject  _gameOverScreen;

    private void OnEnable()
    {
        _player.OnDie += GameOver;
    }

    private void OnDisable()
    {
        _player.OnDie -= GameOver;
    }

    private void GameOver()
    {
        SaveBestScore();
        
        Sequence gameOverSequence = DOTween.Sequence();
        gameOverSequence.AppendInterval(1);
        gameOverSequence.AppendCallback(() => _gameOverScreen.SetActive(true));
    }

    private void SaveBestScore()
    {
        int distanceTraveled = _player.GetDistanceTraveled();

        if (!PlayerPrefs.HasKey(SaveKeys.BEST_SCORE) || distanceTraveled > PlayerPrefs.GetInt(SaveKeys.BEST_SCORE))
        {
            PlayerPrefs.SetInt(SaveKeys.BEST_SCORE, distanceTraveled);
        }
    }
}
