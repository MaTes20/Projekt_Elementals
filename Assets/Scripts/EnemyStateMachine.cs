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
    public float aggroRange = 10f; // Vzdálenost, na kterou nepøítel zaène pronásledovat hráèe
    public float attackRange = 2f; // Vzdálenost, na kterou nepøítel zaène útoèit na hráèe
    public float attackCooldown = 1f; // Doba mezi útoky

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
        // Logika pro hlídání
        if (Vector2.Distance(transform.position, player.position) < aggroRange)
        {
            currentState = EnemyState.Aggro;
        }
    }

    void Aggro()
    {
        // Logika pro pronásledování hráèe
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
            // Pohyb smìrem k hráèi
            transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * 3f);
        }
    }

    void Attacking()
    {
        // Logika pro útok na hráèe
        if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            currentState = EnemyState.Aggro;
        }
        else if (attackCooldownTimer <= 0f)
        {
            // Proveïte útok
            Attack();
            attackCooldownTimer = attackCooldown;
        }
    }

    void Attack()
    {
        // Zde pøidejte kód pro útok na hráèe
        Debug.Log("Nepøítel útoèí na hráèe!");
    }
}
