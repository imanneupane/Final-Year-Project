using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIscript : MonoBehaviour
{
    public Transform track;
    //public Transform trackEvolve;
    public float maxSteerAngle = 45f;

    public WheelCollider wheelFL;
    public WheelCollider wheelFR;

    private List<Transform> nodes;
    private int currentNode = 0;
    void Start()
    {
        Transform[] pathTransforms = track.GetComponentsInChildren<Transform>();

        nodes = new List<Transform>();


        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != track.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    void FixedUpdate()
    {
        ApplySteer();
        Drive();
        FollowWaypoint();
    }

    
    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    private void Drive()
    {
        wheelFL.motorTorque = 1000f;
        wheelFR.motorTorque = 1000f;
    }

    private void FollowWaypoint()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 10f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }
}
