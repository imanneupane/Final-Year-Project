using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLineSc : MonoBehaviour
{
    public CarController movement;
    public WheelsScript[] wheelMovement;
    public Text finishtxt;

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
        }
    }
}
