using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    private PoolableObjectTypes _objectType;
    private bool _isReadyToBeUsed = false;

    private void OnDisable()
    {
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (!_isReadyToBeUsed)
        {
            return;
        }
        
        ObjectPooler.Instance.ReturnObjectToPool(gameObject);
    }

    public void SetObjectType(PoolableObjectTypes objectType)
    {
        _objectType = objectType;
    }

    public PoolableObjectTypes GetObjectType()
    {
        return _objectType;
    }

    public void SetReadyToUse()
    {
        _isReadyToBeUsed = true;
    }
}
