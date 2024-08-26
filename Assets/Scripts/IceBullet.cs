using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : MonoBehaviour
{

    public float speed = 13f;
    public int damage = 10;
    public float freezeDuration = 3f;
    public float freezeAmount = 0.5f;

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
            enemy.TakeDamage(damage);
            enemy.StartFreezing(freezeAmount, freezeDuration); // Použijeme novou metodu StartFreezing
        }
        Destroy(gameObject);
    }
}
