using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    [SerializeField] private Transform transformFL;
    [SerializeField] private Transform transformFR;
    [SerializeField] private Transform transformBL;
    [SerializeField] private Transform transformBR;

    [SerializeField] private WheelCollider colliderFL;
    [SerializeField] private WheelCollider colliderFR;
    [SerializeField] private WheelCollider colliderBL;
    [SerializeField] private WheelCollider colliderBR;

    [SerializeField] private float force;
    [SerializeField] private float maxAngle;

    [SerializeField] private float stepAngle;
    [SerializeField] private float multBackStepAngle;


    private Rigidbody rb;

    public Vector3 centerMass = new Vector3();
    void Start()
    {
        Debug.Log("start");
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerMass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("2nd");
        colliderFL.motorTorque = Input.GetAxis("Vertical") * force; // ÌÎÙÍÎÑÒÜ ÐÀÇÃÎÍÀ
        colliderFR.motorTorque = Input.GetAxis("Vertical") * force;
        colliderBL.motorTorque = Input.GetAxis("Vertical") * force; // ÌÎÙÍÎÑÒÜ ÐÀÇÃÎÍÀ
        colliderBR.motorTorque = Input.GetAxis("Vertical") * force;


        if (Input.GetKey(KeyCode.Space))
        {
            colliderBL.brakeTorque = 1500f;
            colliderBR.brakeTorque = 1500f;
        }
        else
        {
            colliderFL.brakeTorque = 0f;
            colliderFR.brakeTorque = 0f;
            colliderBL.brakeTorque = 0f;
            colliderBR.brakeTorque = 0f;
        }

        float turn = 0f;
        turn = Input.GetAxis("Horizontal"); // ÍÀÏÈÑÀË ÍÅÊÐÀÑÈÂÎ, ÍÅ ÐÀÁÎÒÀÅÒ ÄËß ÄÆÎÉÑÒÈÊÀ
        if (turn != 0f)
        {
            float probAngle; // 50 ÎÁÍÎÂËÅÍÈÉ ÔÈÇÈÊÈ Â ÑÅÊÓÍÄÓ ÏÎ ÓÌÎË×ÀÍÈÞ
            if (turn > 0)
            {
                probAngle = colliderFL.steerAngle + stepAngle;
                if (probAngle > maxAngle)
                {
                    colliderFL.steerAngle = maxAngle;
                    colliderFR.steerAngle = maxAngle;

                }
                else
                {
                    colliderFL.steerAngle = probAngle;
                    colliderFR.steerAngle = probAngle;
                }
            }
            else
            {
                probAngle = colliderFL.steerAngle - stepAngle;
                if (probAngle < -maxAngle)
                {
                    colliderFL.steerAngle = -maxAngle;
                    colliderFR.steerAngle = -maxAngle;

                }
                else
                {
                    colliderFL.steerAngle = probAngle;
                    colliderFR.steerAngle = probAngle;
                }
            }

        }
        else
        {
            float probAngle;
            if (colliderFL.steerAngle < 0)
            {
                probAngle = colliderFL.steerAngle + stepAngle * multBackStepAngle;
                if (probAngle > 0)
                {
                    colliderFL.steerAngle = 0;
                    colliderFR.steerAngle = 0;
                }
                else
                {
                    colliderFL.steerAngle = probAngle;
                    colliderFR.steerAngle = probAngle;
                }
            }
            if (colliderFL.steerAngle > 0)
            {
                probAngle = colliderFL.steerAngle - stepAngle * multBackStepAngle;
                if (probAngle < 0)
                {
                    colliderFL.steerAngle = 0;
                    colliderFR.steerAngle = 0;
                }
                else
                {
                    colliderFL.steerAngle = probAngle;
                    colliderFR.steerAngle = probAngle;
                }
            }
        }
 

        /*colliderFL.steerAngle = maxAngle * Input.GetAxis("Horizontal");
        colliderFR.steerAngle = maxAngle * Input.GetAxis("Horizontal");*/

        RotateWheel(colliderFL, transformFL);
        RotateWheel(colliderFR, transformFR);
        RotateWheel(colliderBL, transformBL);
        RotateWheel(colliderBR, transformBR);
    }

    private void RotateWheel(WheelCollider collider, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        transform.rotation = rotation;
        transform.position = position;
    }

}
