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



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShootFire();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShootIce();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShootStone();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ShootWind();
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
