using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBullet : MonoBehaviour
{
    public float speed = 25f;
    public int damage = 15;
    public float pushForce = 10f; // Zvýšená síla pro lepší viditelnost

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

            // Vypoèítáme smìr, kterým nepøítele odhodíme
            Vector2 pushDirection = enemy.transform.position - transform.position;

            // Debug logy pro kontrolu
            Debug.Log("Enemy hit! Push Direction: " + pushDirection.normalized);
            Debug.Log("Applied Force: " + pushDirection.normalized * pushForce);

            // Volání metody pro posunutí nepøítele
            enemy.StartPushing(pushDirection.magnitude * pushForce, 0.5f);
        }
        Destroy(gameObject);
    }
}
