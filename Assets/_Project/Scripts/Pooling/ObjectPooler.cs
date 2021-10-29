using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPooler : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public PoolableObjectTypes objectType;
        public int size;
        public GameObject[] prefabsToBeSpawned;
    }

    public static ObjectPooler Instance { get; private set; }

    [SerializeField] private List<Pool> _pools;
    
    private Dictionary<PoolableObjectTypes, Queue<GameObject>> _poolDictionary;

    private void Awake()
    {
        InitializePooler();
    }

    private void InitializePooler()
    {
        Instance = this;
        
        _poolDictionary = new Dictionary<PoolableObjectTypes, Queue<GameObject>>();

        foreach (Pool pool in _pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            _poolDictionary.Add(pool.objectType, objectPool);

            for (int i = 0; i < pool.size; i++)
            {
                GameObject newObj = CreateNewObject(pool, pool.prefabsToBeSpawned[Random.Range(0, pool.prefabsToBeSpawned.Length)]);
                AddObjectToPool(pool, newObj);
            }
        }
    }

    public GameObject GetObject(PoolableObjectTypes objectType)
    {
        Pool pool = GetPool(objectType);

        if (!_poolDictionary.ContainsKey(pool.objectType))
        {
            Debug.LogWarning($"There is no pool of type {objectType}");
            return null;
        }

        if (_poolDictionary[objectType].Count > 0)
        {
            return _poolDictionary[objectType].Dequeue();
        }
        else
        {
            return CreateNewObject(pool, pool.prefabsToBeSpawned[Random.Range(0, pool.prefabsToBeSpawned.Length)]);
        }
    }

    private GameObject CreateNewObject(Pool pool, GameObject objPrefab)
    {
        GameObject obj = Instantiate(objPrefab);
        obj.SetActive(false);

        if (obj.TryGetComponent(out PoolableObject poolableObject))
        { 
            poolableObject.SetObjectType(pool.objectType);
            poolableObject.SetReadyToUse();
        }
        else
        {
            PoolableObject poolableObj = obj.AddComponent<PoolableObject>();
            poolableObj.SetObjectType(pool.objectType);
            poolableObj.SetReadyToUse();
        }

        return obj;
    }

    private void AddObjectToPool(Pool pool, GameObject obj)
    {
        if (_poolDictionary.ContainsKey(pool.objectType))
        {
            _poolDictionary[pool.objectType].Enqueue(obj);
        }
    }

    private Pool GetPool(PoolableObjectTypes objectType)
    {
        return _pools.First(p => p.objectType == objectType);
    }

    public void ReturnObjectToPool(GameObject pooledObject)
    {
        PoolableObjectTypes objectType = pooledObject.GetComponent<PoolableObject>().GetObjectType();
        
        if (_poolDictionary.ContainsKey(objectType))
        {
            _poolDictionary[objectType].Enqueue(pooledObject);
        }
        else
        {
            Debug.LogWarning("Object does not belong to a pool.");
        }
    }
}
