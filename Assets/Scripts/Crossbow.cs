using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{

    public GameObject bullet;
    public Transform bulletPos;

    private float timer;
    private GameObject player;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);

        if(distance < 15)
        {
            RotateTowardsPlayer();
            timer += Time.deltaTime;

            if(timer > 2)
             {
                timer = 0;
                shoot();
             }

        }

    }
    void RotateTowardsPlayer()
    {
        // Calculate the direction to the player
        Vector3 direction = player.transform.position - transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Adjust the angle by adding 90 degrees if necessary
        // Change the offset value depending on your sprite's default orientation
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 270));
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, bulletPos.rotation);
    }
}
