using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 15;
    public float burnDuration = 5f; 
    public float burnDamagePerSecond = 5f; 
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
            // Deal immediate damage
            enemy.TakeDamage(damage);

            // Start burning effect
            enemy.StartBurning(burnDamagePerSecond, burnDuration);
        }

        // Destroy the bullet after impact
        Destroy(gameObject);
    }

}
