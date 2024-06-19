using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float moveSpeed = 2f;
    private float originalMoveSpeed;



    public ScoreManager scoreManager;
    public Healthbar healthbar;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        scoreManager = FindObjectOfType<ScoreManager>();
        originalMoveSpeed = moveSpeed;

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
        StartCoroutine(Burn(damagePerSecond, duration));
    }

    private IEnumerator Burn(float damagePerSecond, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            TakeDamage(Mathf.RoundToInt( damagePerSecond * Time.deltaTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void StartSlowing(float slowAmount, float duration)
    {
        StartCoroutine(Slow(slowAmount, duration));
    }

    private IEnumerator Slow(float slowAmount, float duration)
    {
        moveSpeed *= slowAmount;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalMoveSpeed;
    }

}
