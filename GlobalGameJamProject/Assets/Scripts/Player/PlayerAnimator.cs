using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        Player player = GetComponent<Player>();
        player.OnMove += UpdateAnimatorIdle;
        player.OnGrounded += UpdateAnimatorGrounded;
        player.OnVerticalVelocity += UpdateAnimatorVerticalVelocity;
        player.OnClimbing += UpdateAnimatorClimbing;
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

    private void UpdateAnimatorClimbing(bool isClimbing)
    {
        animator.SetBool("IsClimbing", isClimbing);
    }
}
