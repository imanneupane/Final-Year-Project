﻿
using UnityEngine;
using UnityEngine.Animations;

public class CameraFollow : MonoBehaviour
{
    public Transform car;
    public Vector3 offset;
    private Space offsetPS = Space.Self;
    public float smooth = 0.5f;
    private bool lookAt = true; 
   
    void FixedUpdate()
    {
        if (offsetPS == Space.Self)
        {
            transform.position = car.TransformPoint(offset);
        }
        else
        {
            Vector3 newPosition = car.position + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, newPosition, smooth);
            transform.position = smoothPosition;
        }

        if (lookAt)
        {
            transform.LookAt(car);
        }
        else
        {
            transform.rotation = car.rotation;
        }
    }
}
