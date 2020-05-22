using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineSc : MonoBehaviour
{
    public CarController movement;
    public WheelsScript[] wheelMovement;
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.name == "FinishLine")
        {
            movement.enabled = false;
            foreach (WheelsScript w in wheelMovement)
            {
                w.enabled = false;
            }
            Debug.Log("Finish");
        }
    }
}
