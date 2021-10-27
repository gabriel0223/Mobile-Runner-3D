using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void PlayAnimation(string animName, float transitionDuration)
    {
        _anim.CrossFade(animName, transitionDuration);
    }
}
