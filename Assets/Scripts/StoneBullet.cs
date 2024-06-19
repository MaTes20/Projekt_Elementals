using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBullet : MonoBehaviour
{

    public float speed = 15f;
    public int damage = 30;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}