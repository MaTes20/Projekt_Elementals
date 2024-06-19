using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : MonoBehaviour
{

    public float speed = 13f;
    public int damage = 20;
    public float slowDuration = 3f; 
    public float slowAmount = 0.5f;

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
            enemy.StartSlowing(slowAmount, slowDuration);

        }
        Destroy(gameObject);
    }
}
