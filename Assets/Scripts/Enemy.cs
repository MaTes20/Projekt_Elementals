using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float moveSpeed = 2f;
    private float originalMoveSpeed;

    private EnemyStateMachine stateMachine;
    public Rigidbody2D rb;
    private GameObject player;

    public ScoreManager scoreManager;
    public Healthbar healthbar;

    private Coroutine burnCoroutine;  // Track the burning effect
    private Coroutine slowCoroutine;  // Track the slow effect
    private Coroutine stopCoroutine;  // Track the stop effect

    public ParticleSystem fireEffect;
    public ParticleSystem freezeEffect;
    public ParticleSystem hitEffect;
    public ParticleSystem windEffect;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        scoreManager = FindObjectOfType<ScoreManager>();
        originalMoveSpeed = moveSpeed;
        stateMachine = GetComponent<EnemyStateMachine>();


        // Ujistìte se, že Particle System je vypnutý na zaèátku
        if (fireEffect != null)
        {
            fireEffect.Stop();
        }

        if (freezeEffect != null)
        {
            freezeEffect.Stop();
        }

        if (hitEffect != null)
        {
            hitEffect.Stop();
        }

        if (windEffect != null)
        {
            windEffect.Stop();
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            
        }
        healthbar.SetHealth(currentHealth);
    }

    void Die()
    {
        Debug.Log("Enemy died");
        scoreManager.AddPoint();

        Destroy(gameObject);
    }


    public void StartBurning(float damagePerSecond, float duration)
    {
        
        if (burnCoroutine != null)
        {
            StopCoroutine(burnCoroutine);
        }

        if (fireEffect != null)
        {
            // Spus Particle System, když nepøítel zaène hoøet
            fireEffect.Play();
        }

        burnCoroutine = StartCoroutine(Burn(damagePerSecond, duration));
    }

    private IEnumerator Burn(float damagePerSecond, float duration)
    {
        float elapsedTime = 0;
        float damageInterval = 1f; // Apply damage every 1 second

        while (elapsedTime < duration)
        {
            TakeDamage(Mathf.RoundToInt(damagePerSecond * damageInterval));
            yield return new WaitForSeconds(damageInterval);
            elapsedTime += damageInterval;
        }
        {
            fireEffect.Stop();
        }
        burnCoroutine = null;
        
    }

    public void StartSlowing(float slowAmount, float duration)
    {
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
        }

        slowCoroutine = StartCoroutine(Slow(slowAmount, duration));
    }

    private IEnumerator Slow(float slowAmount, float duration)
    {
        moveSpeed *= slowAmount;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalMoveSpeed;
        slowCoroutine = null;
    }




    public void StartStopping(float duration)
    {
        Debug.Log("StartStopping called. Stopping enemy for " + duration + " seconds.");
        if (stateMachine.currentState != EnemyStateMachine.EnemyState.Stopped)
        {
            stateMachine.currentState = EnemyStateMachine.EnemyState.Stopped;

            if (hitEffect != null)
            {
                hitEffect.Play();
            }

            StartCoroutine(StopMovement(duration));
        }
    }

    private IEnumerator StopMovement(float duration)
    {
        Debug.Log("StopMovement Coroutine started.");
        float originalSpeed = moveSpeed;
        moveSpeed = 0; // Stop the enemy by setting its speed to 0
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        Debug.Log("Enemy has stopped. Speed set to 0.");

        yield return new WaitForSeconds(duration);

        moveSpeed = originalSpeed; // Resume the enemy's original speed after stopping
        stateMachine.currentState = EnemyStateMachine.EnemyState.Aggro; // Return to Aggro state or any other appropriate state
        Debug.Log("Enemy resumes movement. Speed restored.");


        if (hitEffect != null)
        {
            hitEffect.Stop();
        }
    }



    public void StartFreezing(float freezeAmount, float duration)
    {
        if (stateMachine.currentState != EnemyStateMachine.EnemyState.Stopped)
        {
            Debug.Log("Enemy freezing by " + (freezeAmount * 100) + "% for " + duration + " seconds.");

            if (freezeEffect != null)
            {
                freezeEffect.Play();
            }

            StartCoroutine(Freeze(freezeAmount, duration)); // Použijeme novou metodu Freeze
        }

       

    }

    private IEnumerator Freeze(float freezeAmount, float duration)
    {
        float originalSpeed = stateMachine.speed;
        stateMachine.speed = originalSpeed * freezeAmount; // Zpomalení nepøítele
        Debug.Log("Enemy frozen to " + stateMachine.speed);

        yield return new WaitForSeconds(duration);

        stateMachine.speed = originalSpeed; // Obnovení pùvodní rychlosti nepøítele
        Debug.Log("Enemy speed restored to " + stateMachine.speed);

        if (freezeEffect != null)
        {
            freezeEffect.Stop();
        }

    }



    public void StartPushing(float forceAmount, float duration)
    {
        if (windEffect != null)
        {
            windEffect.Play();
        }
        StartCoroutine(Push(forceAmount, duration));
    }

    private IEnumerator Push(float forceAmount, float duration)
    {
        float elapsedTime = 0;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector2 pushDirection = (transform.position - player.transform.position).normalized;

        if (rb != null)
        {
            while (elapsedTime < duration)
            {
                // Zde mùžeš aplikovat sílu na základì toho, co je v tvém testu
                Debug.Log("Applying push force: " + forceAmount);
                rb.AddForce(pushDirection * forceAmount, ForceMode2D.Impulse);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            Debug.LogError("No Rigidbody2D found on enemy!");
        }

        if (windEffect != null)
        {
            windEffect.Stop();
        }
    }
}
