using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeIdleAnimation : MonoBehaviour
{
    Animator myAnimator;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        AnimatorStateInfo state = myAnimator.GetCurrentAnimatorStateInfo(0);
        myAnimator.Play(state.fullPathHash, -1, Random.Range(0f, 01f));
    }
}
