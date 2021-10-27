using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.GetComponent<PlayerController>()) return;

        other.gameObject.GetComponent<PlayerController>().Die();
    }
}
