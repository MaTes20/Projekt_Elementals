using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;
    private bool isRunning = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            if (countDown)
            {
                currentTime -= Time.deltaTime;
                if (currentTime <= 0f)
                {
                    currentTime = 0f; // Ensure timer does not go negative
                    isRunning = false; // Stop the timer
                }
            }
            else
            {
                currentTime += Time.deltaTime;
            }

            SetTimerText();
        }
    }

    private void SetTimerText()
    {
        timerText.text = currentTime.ToString("0.00");
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
