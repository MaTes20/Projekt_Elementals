using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBullet : MonoBehaviour
{
    public float speed = 15f;
    public int damage = 30;
    public float stopDuration = 2f;  // Duration for which the enemy's movement will be stopped
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // Apply damage to the enemy
            enemy.StartStopping(stopDuration); // Stop the enemy's movement for a duration
        }
        Destroy(gameObject); // Destroy the bullet after hitting the enemy
    }
}
