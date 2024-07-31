using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBullet : MonoBehaviour
{

    public float speed = 25f;
    public int damage = 25;
    public float pushForce = 5f;
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
            Vector2 pushDirection = enemy.transform.position - transform.position;
            enemy.GetComponent<Rigidbody2D>().AddForce(pushDirection.normalized * pushForce, ForceMode2D.Impulse);
        }
        Destroy(gameObject);
    }
}
