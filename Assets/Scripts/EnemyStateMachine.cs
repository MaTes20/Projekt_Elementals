using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public enum EnemyState
    {
        Guarding,
        Aggro,
        Attacking,
        Stopped  // New state for when the enemy is stopped

    }
    public EnemyState currentState;
    public Transform player; // Odkazuje na objekt hráèe
    public float aggroRange = 10f;
    public float attackRange = 5f;
    public float attackCooldown = 1f;
    public int attackDamage = 20; // Poškození útoku nepøítele
    public LayerMask groundLayer;
    public float groundCheckDistance = 1f;
    public Transform groundCheckPoint;
    public float speed = 2f; // Rychlost pohybu nepøítele
    public float gravityForce = 5f; // Síla gravitace pro udržení na zemi

    private float attackCooldownTimer;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private Animator animator;



    void Start()
    {
        currentState = EnemyState.Guarding;
        attackCooldownTimer = 0f;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        attackCooldownTimer -= Time.deltaTime;

        switch (currentState)
        {
            case EnemyState.Guarding:
                Guarding();
                break;
            case EnemyState.Aggro:
                Aggro();
                break;
            case EnemyState.Attacking:
                Attacking();
                break;
            case EnemyState.Stopped:
                // Enemy is stopped, do nothing
                rb.velocity = Vector2.zero; // Ensure enemy is not moving
                rb.angularVelocity = 0f;
                animator.SetBool("IsWalking", false);
                break;
        }
    }

    void Guarding()
    {

        animator.SetBool("IsWalking", false);
        if (Vector2.Distance(transform.position, player.position) < aggroRange)
        {
            currentState = EnemyState.Aggro;
        }
    }

    void Aggro()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        animator.SetBool("IsWalking", true);

        if (distanceToPlayer < attackRange)
        {
            currentState = EnemyState.Attacking;
            Debug.Log("Nepøítel pøešel do stavu Attacking.");
        }
        else if (distanceToPlayer > aggroRange)
        {
            currentState = EnemyState.Guarding;
            Debug.Log("Nepøítel se vrátil do stavu Guarding.");
        }
        else
        {
            MoveTowardsPlayer();
            animator.SetBool("IsWalking", true);  // Start walking animation
        }
    }

    void Attacking()
    {

        if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            // Pokud hráè opustí dosah útoku, pøepnìte nepøítele zpìt do stavu Aggro
            currentState = EnemyState.Aggro;
            Debug.Log("Nepøítel se vrátil do stavu Aggro.");
        }
        else
        {
            // Nepøítel útoèí, pokud cooldown vypršel
            if (attackCooldownTimer <= 0f)
            {
                Attack();
                attackCooldownTimer = attackCooldown;
                animator.SetTrigger("Attack");  // Spuste animaci útoku
                Debug.Log("Nepøítel útoèí.");
            }
        }
    }

    void Attack()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(); // Udìlení poškození hráèi
        }
        Debug.Log("Nepøítel útoèí na hráèe a udìluje poškození!");
    }

    void MoveTowardsPlayer()
    {
        if (IsGrounded())
        {
            Vector2 direction = (player.position - transform.position).normalized;

            // Nastavení rychlosti
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

            // Otoèení nepøítele
            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                Flip();
            }
        }
        else
        {
            ApplyGravity();
        }
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    void ApplyGravity()
    {
        rb.AddForce(Vector2.down * gravityForce);
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

   

    private void OnDrawGizmosSelected()
    {
        if (groundCheckPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheckPoint.position, groundCheckPoint.position + Vector3.down * groundCheckDistance);
    }

}
