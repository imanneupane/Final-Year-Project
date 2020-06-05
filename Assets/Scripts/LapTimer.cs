using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimer : MonoBehaviour
{
    public static LapTimer instance;
    public Text timeCount;

    private TimeSpan timeStart;
    private bool timeGo;
    private float elapsedTime;

    private void Awake()
    {
        
    }
    void Start()
    {
        timeCount.text = "Time: 00.00.00";
        timeGo = false;
    }

    public void BeginTimer()
    {
        timeGo = true;
        elapsedTime = 0f;
        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timeGo = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timeGo)
        {
            elapsedTime += Time.deltaTime;
            timeStart = TimeSpan.FromSeconds(elapsedTime);
            string timeStartGo = "Time: " + timeStart.ToString("mm'.'ss'.'ff");
            timeCount.text = timeStartGo;
            yield return null;
        }
    }
}
