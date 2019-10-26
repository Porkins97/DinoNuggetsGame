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
        StartCoroutine(TimerCountdown((timeMinutes * 100f) + timeSeconds));
    }

    IEnumerator TimerCountdown(float timeTaken)
    {
        while (countingDown)
        {
            timeTaken -= Time.deltaTime;
            if(timeTaken > 0f)
            {
                timeMinutes = timeTaken / 100f;
                timeSeconds = Mathf.Repeat(timeMinutes, 1.0f) * 100f;
            }
            else
            {
                countingDown = false;
            }

            _timerText.text = string.Format("{0:00} : {1:00}", timeMinutes, timeSeconds);
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
