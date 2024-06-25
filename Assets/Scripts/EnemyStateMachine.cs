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
    public Transform player;
    public float aggroRange = 10f; // Vzd�lenost, na kterou nep��tel za�ne pron�sledovat hr��e
    public float attackRange = 2f; // Vzd�lenost, na kterou nep��tel za�ne �to�it na hr��e
    public float attackCooldown = 1f; // Doba mezi �toky

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
        // Logika pro hl�d�n�
        if (Vector2.Distance(transform.position, player.position) < aggroRange)
        {
            currentState = EnemyState.Aggro;
        }
    }

    void Aggro()
    {
        // Logika pro pron�sledov�n� hr��e
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
            // Pohyb sm�rem k hr��i
            transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * 3f);
        }
    }

    void Attacking()
    {
        // Logika pro �tok na hr��e
        if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            currentState = EnemyState.Aggro;
        }
        else if (attackCooldownTimer <= 0f)
        {
            // Prove�te �tok
            Attack();
            attackCooldownTimer = attackCooldown;
        }
    }

    void Attack()
    {
        // Zde p�idejte k�d pro �tok na hr��e
        Debug.Log("Nep��tel �to�� na hr��e!");
    }
}
