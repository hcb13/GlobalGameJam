using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;

    [SerializeField]
    private float jumpForce = 2.5f;

    private Rigidbody2D myRigidbody;
    private Vector2 inputVector;
    private bool jumping;

    public Action<float> OnMove = delegate { };

    private void Awake()
    {
        myRigidbody= GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputVector = Vector2.zero;

        inputVector.x = Input.GetAxisRaw("Horizontal");
        Move();

        jumping = Input.GetKeyDown(KeyCode.Space);
        Jump();
    }

    private void Move()
    {
        if (inputVector.x != 0f)
        {
            inputVector = inputVector.normalized;

            transform.right = inputVector;
            
        }
        transform.position += (Vector3)inputVector * moveSpeed * Time.deltaTime;
        OnMove?.Invoke(inputVector.x);
    }

    private void Jump()
    {
        if (jumping)
        {
            myRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
   
}
