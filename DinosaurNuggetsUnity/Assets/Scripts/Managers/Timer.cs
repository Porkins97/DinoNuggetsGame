using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    //Serialized variables
    [Header("Timer Settings")]
    [SerializeField] private float timeMinutes;
    [SerializeField] private float timeSeconds;
    [Header("Linked Settings")]
    [SerializeField] private TextMeshProUGUI _timerText;
    //private variables
    private bool countingDown = true;
    private DinoSceneManager _DSM;
    void Start()
    {
        if (_DSM == null)
        {
            _DSM = GameObject.FindGameObjectWithTag("Manager").GetComponent<DinoSceneManager>();
        }
        StartCoroutine(TimerCountdown((timeMinutes * 60f) + timeSeconds));
    }
    IEnumerator TimerCountdown(float timeTaken)
    {
        while (countingDown)
        {
            timeTaken -= Time.deltaTime;
            if(timeTaken > 0f)
            {
                timeMinutes = timeTaken / 60f;
                timeSeconds = Mathf.Repeat(timeMinutes, 1.0f) * 60f;
                if(timeTaken < 61f)
                {
                    if(timeTaken < 16f)
                    {
                       _timerText.faceColor = new Color32(252, 61, 3, 255); //red
                    }
                    else
                    {
                        _timerText.faceColor = new Color32(252, 227, 3,255); //yellow
                    }
                }
            }
            else
            {
                countingDown = false;
            }
            _timerText.text = string.Format("{0:00} : {1:00}", Mathf.Floor(timeMinutes), Mathf.Floor(timeSeconds));
            yield return null;
        }
        if (!countingDown)
        {
            TimerEnded();
        }
    }

    void TimerEnded()
    {
        Debug.Log("Timer Done");
        StopAllCoroutines();
        _DSM.TimeUp();
    }
}
