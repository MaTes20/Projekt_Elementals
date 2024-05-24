using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    private int currentHealth;
    public int damage = 20;

    public Timer timer;
    public Healthbar healthbar;
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
            Debug.Log("Hit");
        }
    }

    private void TakeDamage()
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        healthbar.SetHealth(currentHealth);

    }

    private void Die()
    {
        Debug.Log("Umøel jsi");
        PlayerManager.isGameOver = true;
        gameObject.SetActive(false);
        timer.StopTimer();

    }
}
