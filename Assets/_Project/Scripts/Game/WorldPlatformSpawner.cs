using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPlatformSpawner : MonoBehaviour
{
    [SerializeField] private Transform worldPlatformsContainer;
    [SerializeField] private float _spaceBetweenPlatforms;
    [Tooltip("The number of world platforms the game will keep active at the same time")]
    [SerializeField] private int _maxNumberOfActivePlatforms;

    private ObjectPooler _objectPooler;
    private List<GameObject> _activePlatforms = new List<GameObject>();
    
    public Action OnSpawnWorldPlatform;

    // Start is called before the first frame update
    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        
        SpawnWorldPlatforms(_maxNumberOfActivePlatforms);
    }

    private void SpawnWorldPlatforms(int numberOfPlatforms)
    {
        float zPos;

        if (_activePlatforms.Count > 0)
        {
            //zPos of the last spawned platform + space needed to spawn the next one
            zPos = _activePlatforms[_activePlatforms.Count - 1].transform.position.z + _spaceBetweenPlatforms; 
        }
        else
        {
            zPos = 0;
        }

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            GameObject spawnedPlatform = _objectPooler.GetObject(PoolableObjectTypes.WorldPlatforms);
            spawnedPlatform.transform.position = new Vector3(17.65f, 0.2f, zPos);
            spawnedPlatform.transform.SetParent(worldPlatformsContainer);
            spawnedPlatform.SetActive(true);
            
            _activePlatforms.Add(spawnedPlatform);
            
            if (_activePlatforms.Count > _maxNumberOfActivePlatforms)
                DisableOldWorldPlatform();
            
            zPos += _spaceBetweenPlatforms;
        }
        
        transform.localPosition += Vector3.forward * _spaceBetweenPlatforms;

        OnSpawnWorldPlatform?.Invoke();
    }

    private void DisableOldWorldPlatform()
    {
        _activePlatforms[0].SetActive(false);
        _activePlatforms.RemoveAt(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<PlayerController>()) return;
        
        SpawnWorldPlatforms(1);
    }
}
