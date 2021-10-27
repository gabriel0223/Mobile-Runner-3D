using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    
    private ObjectPooler _objectPooler;
    private List<GameObject> _activeObstacles = new List<GameObject>();
    
    [Header("REFERENCES")]
    [SerializeField] private Transform player;
    [SerializeField] private Lanes _lanes;
    [SerializeField] private WorldPlatformSpawner _worldPlatformSpawner;
    
    [Header("SETTINGS")]
    [SerializeField] private float _minSpaceBetweenSpawns;
    [SerializeField] private float _maxSpaceBetweenSpawns;
    [Tooltip("The number of obstacles the game will keep active at the same time")]
    [SerializeField] private int _maxNumberOfActiveObstacles;
    [Tooltip("How far behind the player the obstacle has to be for it to be deleted")] [SerializeField]
    private float _distanceToDelete;

    private int lastSpawnedLane;

    private void OnEnable()
    {
        _worldPlatformSpawner.OnSpawnWorldPlatform += ReplaceOldObstacles;
    }

    private void OnDisable()
    {
        _worldPlatformSpawner.OnSpawnWorldPlatform -= ReplaceOldObstacles;
    }

    // Start is called before the first frame update
    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        
        SpawnObstacles(_maxNumberOfActiveObstacles);
    }

    private void SpawnObstacles(int numberOfObstacles)
    {
        float zPos;
        
        if (_activeObstacles.Count > 0)
        {
            float randomSpaceBetweenPlatforms = Random.Range(_minSpaceBetweenSpawns, _maxSpaceBetweenSpawns);
            
            //zPos of the last spawned obstacle + space needed to spawn the next one
            zPos = _activeObstacles[_activeObstacles.Count - 1].transform.position.z + randomSpaceBetweenPlatforms; 
        }
        else
        {
            zPos = 0;
        }

        for (int i = 0; i < numberOfObstacles; i++)
        {
            Vector3 obstaclePosition = new Vector3(GetRandomLaneXPos(), 0, zPos);
            
            GameObject spawnedObstacle = _objectPooler.SpawnFromPool("Obstacles", obstaclePosition, Quaternion.identity);
            _activeObstacles.Add(spawnedObstacle);

            if (_activeObstacles.Count > _maxNumberOfActiveObstacles)
            {
                ReplaceOldObstacles();
            }

            zPos += Random.Range(_minSpaceBetweenSpawns, _maxSpaceBetweenSpawns);
        }

        float GetRandomLaneXPos()
        {
            int randomLane = Random.Range(0,  _lanes.LanesList.Length);
            
            while (randomLane == lastSpawnedLane)
            {
                randomLane = Random.Range(0,  _lanes.LanesList.Length);
            }

            lastSpawnedLane = randomLane;

            return _lanes.LanesList[randomLane].Position.x;
        }
    }
    
    private void ReplaceOldObstacles()
    {
        //list of obstacles behind the player
        GameObject[] oldObstacles = _activeObstacles.Where(obstacle => player.position.z - obstacle.transform.position.z > _distanceToDelete).ToArray();

        foreach (var obstacle in oldObstacles)
        {
            obstacle.SetActive(false);
            _activeObstacles.Remove(obstacle);
        }
        
        SpawnObstacles(oldObstacles.Length);
    }
}
