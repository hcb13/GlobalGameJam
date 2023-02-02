using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        GetComponent<Player>().OnMove += UpdateAnimatorIdle;
    }

    private void UpdateAnimatorIdle(float movement)
    {
        animator.SetBool("IsIdle", movement == 0);
    }

}
