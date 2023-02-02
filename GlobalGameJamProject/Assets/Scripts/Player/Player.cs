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

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float groundCheckRadius;
    private bool isGrounded;

    private bool _hasEnterLadder = false;
    private Vector2 _vertical;
    private bool isClimbing = false;

    public Action<float> OnMove = delegate { };
    public Action<bool> OnGrounded = delegate { };
    public Action<float> OnVerticalVelocity = delegate { };
    public Action<bool> OnClimbing = delegate { };

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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        Jump();

        inputVector.y = Input.GetAxisRaw("Vertical");
        SetVertical(inputVector.y);
    }

    private void Move()
    {
        if (inputVector.x != 0f)
        {
            inputVector = inputVector.normalized;

            transform.right = inputVector;
            
        }
        transform.position += (Vector3)inputVector * moveSpeed * Time.deltaTime;
        
    }

    private void Jump()
    {        
        if (jumping && isGrounded)
        {
            myRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }        
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            myRigidbody.gravityScale = 0f;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x,
                                              _vertical.normalized.y * moveSpeed);
        }
        else
        {
            myRigidbody.gravityScale = 1;
        }
    }

    private void LateUpdate()
    {
        OnMove?.Invoke(inputVector.x);
        OnGrounded?.Invoke(isGrounded);
        OnVerticalVelocity?.Invoke(myRigidbody.velocity.y);
        OnClimbing?.Invoke(isClimbing);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
        {
            EnterStair();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
        {
            ExitStair();
        }
    }

    private void EnterStair()
    {
        _hasEnterLadder = true;
    }

    private void ExitStair()
    {
        _hasEnterLadder = false;
        isClimbing = false;
    }

    private void SetVertical(float vertical)
    {
        _vertical = new Vector2(0f, vertical);

        if (_hasEnterLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

}
