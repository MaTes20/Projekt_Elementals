using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float xMove = 0;

  

    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private float jumpForce = 10;

    [SerializeField]
    private Transform groundChecker;

    [SerializeField]
    private LayerMask playerLayerMask;

    [SerializeField]
    private Transform cameraholder;

    public Animator animator;
    bool isFacingRight = true;


    public bool isGrounded = false;
    private bool jumpedInAir = false;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        CheckGrounded();
        var yMove = rb.velocity.y;


       

        xMove = 0;
        if (Input.GetKey(KeyCode.D))
        {
            xMove = speed;
            animator.SetFloat("Speed", Mathf.Abs(xMove));
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            xMove = -speed;
            animator.SetFloat("Speed", Mathf.Abs(xMove));

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                yMove = jumpForce;

            }
            else
            {
                if (!jumpedInAir)
                {
                    yMove = jumpForce;
                    jumpedInAir = true;
                }
            }
        }
        rb.velocity = new Vector2(xMove, yMove);
        animator.SetFloat("speed", Mathf.Abs(xMove));

        if (isFacingRight && xMove < 0 || !isFacingRight && xMove > 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
    void CheckGrounded()
    {
        var hit = Physics2D.Raycast(groundChecker.position, Vector2.down, 0.1f, playerLayerMask);
        if (hit.collider == null)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
            jumpedInAir = false;
        }
    }


    

}
