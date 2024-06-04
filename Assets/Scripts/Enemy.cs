using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public ScoreManager scoreManager;
    public Healthbar healthbar;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        scoreManager = FindObjectOfType<ScoreManager>();
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

    
}
