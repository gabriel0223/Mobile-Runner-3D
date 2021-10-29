using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    private PoolableObjectTypes _objectType;
    private bool _isBeingUsed;

    private void OnDisable()
    {
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (!_isBeingUsed)
        {
            return;
        }
        
        ObjectPooler.Instance.ReturnObjectToPool(gameObject);
        _isBeingUsed = false;
    }

    public void SetObjectType(PoolableObjectTypes objectType)
    {
        _objectType = objectType;
    }

    public PoolableObjectTypes GetObjectType()
    {
        return _objectType;
    }

    public void PutToUse()
    {
        _isBeingUsed = true;
    }
}
