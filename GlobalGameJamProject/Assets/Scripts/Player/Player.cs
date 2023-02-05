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

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float timeWaitRespawn = 3f;

    [SerializeField] private AudioSource audioPlayerJump;

    [SerializeField] private GameObject dialogPauseMenu;
    [SerializeField] private bool isPaused = false;

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

        if (!isPaused)
        {
            isPaused = Input.GetKey(KeyCode.Escape);

            inputVector.x = Input.GetAxisRaw("Horizontal");
            Move();

            jumping = Input.GetKeyDown(KeyCode.Space);
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            Jump();

            inputVector.y = Input.GetAxisRaw("Vertical");
            SetVertical(inputVector.y);
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        dialogPauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void NotPause()
    {
        isPaused = false;
        dialogPauseMenu.SetActive(false);
        Time.timeScale = 1;
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
            audioPlayerJump.Play();
            myRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }        
    }

    public void Respawn()
    {
        GameObject LifeHUD = GameObject.FindGameObjectWithTag("LifeHUD");
        LifeHUD.GetComponent<LifeHUD>().UpdateAnimatorLifes(-1);
        StartCoroutine(CoroutineRespawn());
    }

    private IEnumerator CoroutineRespawn()
    {
        yield return new WaitForSeconds(timeWaitRespawn);
        transform.position = spawnPoint.position;
        GetComponent<PlayerAnimator>().UpdateAnimatorHurt(false);
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
        _vertical = new Vector2(0f, vertical / 2 * Time.deltaTime);

        if (_hasEnterLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

}
