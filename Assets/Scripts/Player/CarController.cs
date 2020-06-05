using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelsScript[] wheels;

    //all the measurements in meters 
    [Header("Car Info")]
    public float wheelBase;
    public float rearTrack;
    public float turnRadius;
    
    [Header("Inputs")]
    public float steerInput;

    public float ackermannAngleL;
    public float ackermannAngleR;

    void Update()
    {
        steerInput = Input.GetAxis("Horizontal");
        //turning right
        if (steerInput > 0)
        {
            ackermannAngleL = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
            ackermannAngleR = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
        }
        //turning left
        else if (steerInput < 0)
        {
            ackermannAngleL = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
            ackermannAngleR = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
        }
        else
        {
            ackermannAngleL = 0;
            ackermannAngleR = 0;
        }
        
        foreach (WheelsScript w in wheels)
        {
            if (w.wheelFrontL)
            {
                w.steerAngle = ackermannAngleL;
            }
            if (w.wheelFrontR)
            {
                w.steerAngle = ackermannAngleR;
            }
        }
        
    }
}
