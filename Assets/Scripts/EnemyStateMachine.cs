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
    public Transform player; // Odkazuje na objekt hr��e
    public float aggroRange = 10f;
    public float attackRange = 5f;
    public float attackCooldown = 1f;
    public int attackDamage = 20; // Po�kozen� �toku nep��tele
    public LayerMask groundLayer;
    public float groundCheckDistance = 1f;
    public Transform groundCheckPoint;
    public float speed = 2f; // Rychlost pohybu nep��tele
    public float gravityForce = 5f; // S�la gravitace pro udr�en� na zemi

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
            Debug.Log("Nep��tel p�e�el do stavu Attacking.");
        }
        else if (distanceToPlayer > aggroRange)
        {
            currentState = EnemyState.Guarding;
            Debug.Log("Nep��tel se vr�til do stavu Guarding.");
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
            // Pokud hr�� opust� dosah �toku, p�epn�te nep��tele zp�t do stavu Aggro
            currentState = EnemyState.Aggro;
            Debug.Log("Nep��tel se vr�til do stavu Aggro.");
        }
        else
        {
            // Nep��tel �to��, pokud cooldown vypr�el
            if (attackCooldownTimer <= 0f)
            {
                Attack();
                attackCooldownTimer = attackCooldown;
                animator.SetTrigger("Attack");  // Spus�te animaci �toku
                Debug.Log("Nep��tel �to��.");
            }
        }
    }

    void Attack()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(); // Ud�len� po�kozen� hr��i
        }
        Debug.Log("Nep��tel �to�� na hr��e a ud�luje po�kozen�!");
    }

    void MoveTowardsPlayer()
    {
        if (IsGrounded())
        {
            Vector2 direction = (player.position - transform.position).normalized;

            // Nastaven� rychlosti
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

            // Oto�en� nep��tele
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
