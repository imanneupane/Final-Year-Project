using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIscript : MonoBehaviour
{
    public Transform track;
    public float maxSteerAngle = 45f;

    public WheelCollider wheelFL;
    public WheelCollider wheelFR;

    public float maxMotorTorque = 800f;
    public float currentSpeed;
    public float maxSpeed = 100f;
    public Vector3 centerOfMass;

    [Header("Sensors")]
    public float sensorLength = 3f;
    public Vector3 frontSensor = new Vector3 (0f, 0.2f, 0.5f);
    public float sideSensor = 0.2f;
    public float sensorAngle = 20;

    private List<Transform> nodes;
    private int currentNode = 0;
    private bool avoiding = false;
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
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
        Sensors();
    }

    
    private void ApplySteer()
    {
        if (avoiding) return;
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

        if (currentSpeed < maxSpeed)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
        
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

    private void Sensors()
    {
        Vector3 sensorStart = transform.position + frontSensor;
        sensorStart += transform.forward * frontSensor.z;
        sensorStart += transform.up * frontSensor.y;

        float avoidMultipler = 0;
        avoiding = false;
        //right sensor
        sensorStart += transform.right * sideSensor;
        if (Physics.Raycast(sensorStart, transform.forward, out RaycastHit hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Track"))
            {
                Debug.DrawLine(sensorStart, hit.point);
                avoiding = true;
                avoidMultipler -= 1f;
            }
        }
        
        //rightAngle sensor
        else if (Physics.Raycast(sensorStart, Quaternion.AngleAxis(sensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Track"))
            {
                Debug.DrawLine(sensorStart, hit.point);
                avoiding = true;
                avoidMultipler -= 0.5f;
            }
        }
        
        //left sensor
        sensorStart -= transform.right * 2 * sideSensor;
        if (Physics.Raycast(sensorStart, transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Track"))
            {
                Debug.DrawLine(sensorStart, hit.point);
                avoiding = true;
                avoidMultipler += 1f;
            }
        }
        
        //leftAngle sensor
        else if (Physics.Raycast(sensorStart, Quaternion.AngleAxis(-sensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Track"))
            {
                Debug.DrawLine(sensorStart, hit.point);
                avoiding = true;
                avoidMultipler += 0.5f;
            }
        }
        //Center sensor
        if (avoidMultipler == 0)
        {
            if (Physics.Raycast(sensorStart, transform.forward, out hit, sensorLength))
            {
                if (!hit.collider.CompareTag("Track"))
                {
                    Debug.DrawLine(sensorStart, hit.point);
                    avoiding = true;
                    if(hit.normal.x < 0)
                    {
                        avoidMultipler = -1;
                    }
                    else
                    {
                        avoidMultipler = 1;
                    }
                }
            }

        }

        if (avoiding)
        {
            wheelFL.steerAngle = maxSteerAngle * avoidMultipler;
            wheelFR.steerAngle = maxSteerAngle * avoidMultipler;
        }
        
    }

}
