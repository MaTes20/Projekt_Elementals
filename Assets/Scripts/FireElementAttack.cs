using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElementAttack : MonoBehaviour
{

    public Transform firePoint;
    public GameObject fireElement;
   


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Shoot();
        }
    }


    void Shoot()
    {
        Instantiate(fireElement, firePoint.position, firePoint.rotation);
    }
}
