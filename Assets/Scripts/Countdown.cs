﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public int counter;
    public Text countText;
    public WheelsScript[] wheelMovement;

    // Start is called before the first frame update
    void Start()
    { 
        StartCoroutine(CountdownToStart());
    }

    void Update()
    {
        foreach (WheelsScript w in wheelMovement)
        {
            w.enabled = false;
        }
    }

    IEnumerator CountdownToStart()
    {
        while (counter > 0)
        {
            countText.text = counter.ToString();
            yield return new WaitForSeconds(1f);
            counter--;
        }

        countText.text = "START";
        yield return new WaitForSeconds(1f);
        countText.gameObject.SetActive(false);
        foreach (WheelsScript w in wheelMovement)
        {
            w.enabled = true;
        }
    }    

}