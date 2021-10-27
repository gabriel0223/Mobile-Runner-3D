using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanes : MonoBehaviour
{
    [Serializable]
    public class Lane
    {
        [SerializeField] private Transform _transform;
        public Vector3 Position => _transform.localPosition;
    }

    [SerializeField] private Lane[] _lanesList;
    public Lane[] LanesList => _lanesList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
