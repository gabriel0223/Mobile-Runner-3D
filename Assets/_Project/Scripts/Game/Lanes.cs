using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanes : MonoBehaviour
{
    [SerializeField] private Transform[] _lanesList;
    public Transform[] LanesList => _lanesList;
}
