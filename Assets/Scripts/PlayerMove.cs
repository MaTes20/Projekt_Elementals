using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{


    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;

    [SerializeField] private AudioSource jumpSoundEffect;

    public CoinManager cm;

    public Transform groundCheck; // Transform pro bod kontroly zemì
    public float groundCheckRadius = 0.2f; // Polomìr kontroly zemì
    public LayerMask groundLayer; // Vrstva pro zem

    private bool isGrounded;



    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);


        if (Input.GetButtonDown("Jump") && isGrounded)
        {

            jump = true;
            animator.SetBool("isJumping", true);
            jumpSoundEffect.Play();

        }
    }


    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            cm.coinCount++;
        }
    }

   

    
}
