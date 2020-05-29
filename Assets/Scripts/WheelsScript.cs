using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsScript : MonoBehaviour
{
    private Rigidbody rb;

    public bool wheelFrontL;
    public bool wheelFrontR;
    public bool wheelBackL;
    public bool wheelBackR;

    [Header("Suspension")]
    public float restLength;
    public float springTravel;
    public float damperStiffness;
    public float springStiffness;

    private float minimumL;
    private float maximumL;
    private float finalL;
    private float springForce;
    private float springLength;
    private float springVelocity;
    private float damperForce;

    [Header("Wheels")]
    public float steerAngle;
    private float wheelAngle;
    public float steerTime;

    private Vector3 suspensionForce;
    private Vector3 wheelVelocityLS;
    private float fX;
    private float fY;

    [Header("Wheel")]
    public float wheelRadius;

    public float Minimum { get => minimumL; set => minimumL = value; }

    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();

        maximumL = restLength + springTravel;
        Minimum = restLength - springTravel;
    }
    
    void Update()
    {
        wheelAngle = Mathf.Lerp(wheelAngle, steerAngle, steerTime * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);

        Debug.DrawRay(transform.position, -transform.up * (springLength + wheelRadius), Color.green);
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maximumL + wheelRadius))
        {
            finalL = springLength;
            springLength = hit.distance - wheelRadius;
            springForce = springStiffness * (restLength - springLength);
            springLength = Mathf.Clamp(springLength, minimumL, maximumL);
            springVelocity = (finalL - springLength) / Time.fixedDeltaTime;
            suspensionForce = (springForce + damperForce) * transform.up;

            wheelVelocityLS = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point));
            fX = Input.GetAxis("Vertical") * 0.3f * springForce;
            fY = wheelVelocityLS.x * springForce;

            damperForce = damperStiffness * springVelocity;
            rb.AddForceAtPosition(suspensionForce + (fX * transform.forward) + (fY * -transform.right), hit.point);
        }
    }
}
