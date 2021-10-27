using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPooler : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public string tag;
        public int size;
        public GameObject[] prefabsToBeSpawned;
    }

    public static ObjectPooler Instance;

    [SerializeField] private List<Pool> _pools;
    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    private void Awake()
    {
        Instance = this;
        
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in _pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                int randomNumber = Random.Range(0, pool.prefabsToBeSpawned.Length);
                GameObject obj = Instantiate(pool.prefabsToBeSpawned[randomNumber]);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (_poolDictionary[tag].Count == 0) return null;
        
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return null;
        }
        
        GameObject objectToSpawn = _poolDictionary[tag].Dequeue();
        
        if (objectToSpawn.activeSelf) objectToSpawn.SetActive(false);

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        
        _poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void RemoveFromPool(string tag)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return;
        }

        _poolDictionary[tag].Dequeue();
    }
}
