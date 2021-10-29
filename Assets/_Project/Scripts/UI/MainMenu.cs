using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    
    // Start is called before the first frame update
    private void Start()
    {
        SubscribeToButtons();
    }

    private void SubscribeToButtons()
    {
        _playButton.onClick.AddListener(Play);
        _quitButton.onClick.AddListener(Quit);
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(Play);
        _quitButton.onClick.RemoveListener(Quit);
    }

    private void Play()
    {
        GameManager.Instance.LoadGame();
    }

    private void Quit()
    {
        Application.Quit();
    }
}
