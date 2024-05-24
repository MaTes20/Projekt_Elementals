using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
   public Timer timer;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "DeadZone")
        {
            PlayerManager.isGameOver = true;
            gameObject.SetActive(false);
            timer.StopTimer();
            

        }
        else if (collision.transform.tag == "End")
        {
            PlayerManager.isEnd = true;
            gameObject.SetActive(false);
            timer.StopTimer();
            
        }

    }
}
