using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;

    private Rigidbody2D myRigidbody;
    private Vector2 inputVector;

    public Action<float> OnMove = delegate { };

    private void Awake()
    {
        myRigidbody= GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputVector = Vector2.zero;

        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector = inputVector.normalized;

        transform.right = inputVector;

        OnMove?.Invoke(inputVector.x);
    }

    private void FixedUpdate()
    {
        myRigidbody.velocity = inputVector * moveSpeed * Time.deltaTime;
    }

}
