using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffTrack : MonoBehaviour
{
    public WheelsScript[] wheelMovement;
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Ground")
        {
            foreach(WheelsScript w in wheelMovement)
            {
                w.springStiffness = 15000;
            }
            Debug.Log("We on grass");
        }
        else
        {
            foreach(WheelsScript w in wheelMovement)
            {
                w.springStiffness = 30000;
            }
        }
    }
}
