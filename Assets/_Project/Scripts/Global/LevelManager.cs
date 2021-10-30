using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _progressBar;
    
    private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();

    private void Awake()
    {
        Instance = this;

        SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive);
    }

    public void ReloadScene()
    {
        // int currentScene = SceneManager.GetActiveScene().buildIndex;
        // SceneManager.LoadSceneAsync(currentScene);
        // SceneManager.UnloadSceneAsync(currentScene);

        SceneManager.UnloadSceneAsync((int)SceneIndexes.GAME);
        SceneManager.LoadSceneAsync((int)SceneIndexes.GAME, LoadSceneMode.Additive);
    }

    public void LoadGame()
    {
        StartCoroutine(LoadGameCoroutine());
    }

    public IEnumerator LoadGameCoroutine()
    {
        if (_loadingScreen.activeSelf) yield break;
        
        _loadingScreen.SetActive(true);
        
        _scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.MAIN_MENU));
        _scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.GAME, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
        
        Scene gameScene = SceneManager.GetSceneByBuildIndex((int)SceneIndexes.GAME);
        while (!gameScene.isLoaded)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(gameScene);
    }

    private float _totalSceneProgress;
    private IEnumerator GetSceneLoadProgress()
    {
        foreach (AsyncOperation load in _scenesLoading)
        {
            while (!load.isDone)
            {
                _totalSceneProgress = 0;

                foreach (AsyncOperation operation in _scenesLoading)
                {
                    _totalSceneProgress += operation.progress;
                }

                _totalSceneProgress = (_totalSceneProgress / _scenesLoading.Count) * 100f;
                
                _progressBar.value = Mathf.RoundToInt(_totalSceneProgress);
                
                yield return null;
            }
            
            load.allowSceneActivation = true;
        }
        
        _loadingScreen.gameObject.SetActive(false);
    }
}
