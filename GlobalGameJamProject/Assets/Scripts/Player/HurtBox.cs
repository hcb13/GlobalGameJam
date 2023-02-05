using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource audioHurt;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            player.GetComponent<PlayerAnimator>().UpdateAnimatorHurt(true);
            audioHurt.Play();
            player.GetComponent<Player>().Respawn();
        }
    }
}
