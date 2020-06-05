using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIpathChange : MonoBehaviour
{
    public Transform trackEvolve;
    public float maxSteerAngle = 45f;

    public WheelCollider wheelFL;
    public WheelCollider wheelFR;

    private List<Transform> updatenodes;
    private int currentNode = 0;
    void Start()
    {
        Transform[] pathTransforms = trackEvolve.GetComponentsInChildren<Transform>();

        updatenodes = new List<Transform>();


        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != trackEvolve.transform)
            {
                updatenodes.Add(pathTransforms[i]);
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
        Vector3 relativeVector = transform.InverseTransformPoint(updatenodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    private void Drive()
    {
        wheelFL.motorTorque = 800f;
        wheelFR.motorTorque = 800f;
    }

    private void FollowWaypoint()
    {
        if (Vector3.Distance(transform.position, updatenodes[currentNode].position) < 10f)
        {
            if (currentNode == updatenodes.Count - 1)
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
