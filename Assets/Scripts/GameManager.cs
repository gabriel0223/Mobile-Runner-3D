using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _progressBar;
    
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive);
    }

    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadGame()
    {
        _loadingScreen.SetActive(true);
        
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.MAIN_MENU));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.GAME, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    private float _totalSceneProgress;
    private IEnumerator GetSceneLoadProgress()
    {
        foreach (AsyncOperation load in scenesLoading)
        {
            while (!load.isDone)
            {
                _totalSceneProgress = 0;

                foreach (AsyncOperation operation in scenesLoading)
                {
                    _totalSceneProgress += operation.progress;
                }

                _totalSceneProgress = (_totalSceneProgress / scenesLoading.Count) * 100f;
                
                _progressBar.value = Mathf.RoundToInt(_totalSceneProgress);
                
                yield return null;
            }
        }

        _loadingScreen.gameObject.SetActive(false);
    }
}
