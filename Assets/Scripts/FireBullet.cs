using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
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
            enemy.TakeDamage(damage);
            enemy.StartBurning(burnDamagePerSecond, burnDuration);

        }
        Destroy(gameObject);
    }


}
