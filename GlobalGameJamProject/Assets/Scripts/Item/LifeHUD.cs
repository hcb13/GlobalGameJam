using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHUD : MonoBehaviour
{

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void UpdateAnimatorLifes(int value)
    {
        int aux = animator.GetInteger("Lifes");
        if ((value > 0 && aux < 3) || (value < 0 && aux > 0))
        {
            aux += value;
            animator.SetInteger("Lifes", aux);
        }

        if(value < 0 && aux == 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().Lost();
        }
    }
}
