using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    public CoinManager cm;

    // Start is called before the first frame update
    void Start()
    {
       

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cm.coinCount +=5;
            Destroy(gameObject);
        }
    }
}