using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElementAttack : MonoBehaviour
{

    public Transform firePoint;
    public GameObject fireElement;
    public GameObject iceElement;
    public GameObject stoneElement;
    public GameObject windElement;


    // Cooldowns
    private float fireCooldown = 5f;
    private float iceCooldown = 5f;
    private float stoneCooldown = 5f;
    private float airCooldown = 5f;

    // Timestamps for next available use
    private float nextFireTime = 0f;
    private float nextIceTime = 0f;
    private float nextStoneTime = 0f;
    private float nextAirTime = 0f;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time >= nextFireTime)
        {
            ShootFire();
            nextFireTime = Time.time + fireCooldown;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && Time.time >= nextIceTime)
        {
            ShootIce();
            nextIceTime = Time.time + iceCooldown;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && Time.time >= nextStoneTime)
        {
            ShootStone();
            nextStoneTime = Time.time + stoneCooldown;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && Time.time >= nextAirTime)
        {
            ShootWind();
            nextAirTime = Time.time + airCooldown;
        }
    }


    void ShootFire()
    {
        Instantiate(fireElement, firePoint.position, firePoint.rotation);
    }

    void ShootIce()
    {
        Instantiate(iceElement, firePoint.position, firePoint.rotation);

    }

    void ShootStone()
    {
        Instantiate(stoneElement, firePoint.position, firePoint.rotation);
    }

    void ShootWind()
    {
        Instantiate(windElement, firePoint.position, firePoint.rotation);

    }
}
