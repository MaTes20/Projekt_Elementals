using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public enum EnemyState
    {
        Guarding,
        Aggro,
        Attacking
    }
    public EnemyState currentState;
    public Transform player; // Odkazuje na objekt hráèe
    public float aggroRange = 10f;
    public float attackRange = 2f;
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

    void Start()
    {
        currentState = EnemyState.Guarding;
        attackCooldownTimer = 0f;
        rb = GetComponent<Rigidbody2D>();
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
        }
    }

    void Guarding()
    {
        if (Vector2.Distance(transform.position, player.position) < aggroRange)
        {
            currentState = EnemyState.Aggro;
        }
    }

    void Aggro()
    {
        if (Vector2.Distance(transform.position, player.position) < attackRange)
        {
            currentState = EnemyState.Attacking;
        }
        else if (Vector2.Distance(transform.position, player.position) > aggroRange)
        {
            currentState = EnemyState.Guarding;
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    void Attacking()
    {
        if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            currentState = EnemyState.Aggro;
        }
        else if (attackCooldownTimer <= 0f)
        {
            Attack();
            attackCooldownTimer = attackCooldown;
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
