using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _quitButton;

    private void OnEnable()
    {
        SubscribeToButtons();
    }

    private void OnDisable()
    {
        UnsubscribeToButtons();
    }

    private void SubscribeToButtons()
    {
        _retryButton.onClick.AddListener(Retry);
        _quitButton.onClick.AddListener(Quit);
    }

    private void UnsubscribeToButtons()
    {
        _retryButton.onClick.RemoveListener(Retry);
        _quitButton.onClick.RemoveListener(Quit);
    }

    private void Retry()
    {
        LevelManager.Instance.LoadGame();
    }

    private void Quit()
    {
        Application.Quit();
    }
}
