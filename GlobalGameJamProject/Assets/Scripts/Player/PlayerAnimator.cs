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
        GetComponent<Player>().OnGrounded += UpdateAnimatorGrounded;
        GetComponent<Player>().OnVerticalVelocity += UpdateAnimatorVerticalVelocity;
    }

    private void UpdateAnimatorIdle(float movement)
    {
        animator.SetBool("IsIdle", movement == 0);
    }

    private void UpdateAnimatorGrounded(bool isGrounded)
    {
        animator.SetBool("IsGrounded", isGrounded);
    }

    private void UpdateAnimatorVerticalVelocity(float velocity)
    {
        animator.SetFloat("VerticalVelocity", velocity);
    }
}
