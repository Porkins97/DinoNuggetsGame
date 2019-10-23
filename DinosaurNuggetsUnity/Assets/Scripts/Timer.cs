using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText; // replace with GUI Elements

    public float timeMinutes;
    public float timeSeconds;
    bool countingDown = true;

    void Update()
    {
        if (countingDown == true)
        {
            timeSeconds -= Time.deltaTime;
            if (timeSeconds <= 0f && timeMinutes > 0f)
            {
                timeMinutes--;
                timeSeconds = 59.5f;
            }
            else
            {
                //timeSeconds = 0;
                //TimerEnded();
            }

            timerText.text = string.Format("{0:00} : {1:00}", timeMinutes, timeSeconds);
        }
       
            
    }

    void TimerEnded()
    {
        Debug.Log("Timer Done");
        //countingDown = false;
    }
}
