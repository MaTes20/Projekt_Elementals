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
    public Transform player; // Odkazuje na objekt hr��e
    public float aggroRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    public int attackDamage = 20; // Po�kozen� �toku nep��tele
    public LayerMask groundLayer;
    public float groundCheckDistance = 1f;
    public Transform groundCheckPoint;

    private float attackCooldownTimer;

    void Start()
    {
        currentState = EnemyState.Guarding;
        attackCooldownTimer = 0f;
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
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>(); // Z�sk�n� komponenty PlayerHealth
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
            transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * 3f);
        }
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckDistance, groundLayer);
    }

}
