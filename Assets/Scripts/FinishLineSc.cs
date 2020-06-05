using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLineSc : MonoBehaviour
{
    public CarController movement;
    public WheelsScript[] wheelMovement;
    public Text finishtxt;
    public LapTimer timer;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.name == "FinishLine")
        {
            movement.enabled = false;
            foreach (WheelsScript w in wheelMovement)
            {
                w.enabled = false;
            }
            finishtxt.enabled = true;
            Debug.Log("Finish");
            timer.EndTimer();
            StartCoroutine(Wait()); 
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
