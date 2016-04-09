using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{

    public float power = 1;

    public float yawTorque = 1;
    public float pitchRollTorque = 1;
    public float stability = 0.95f;

    public float throttle;
    public float yaw;
    public float pitch;
    public float roll;
    public float yAngularVelocity;


    private float offset = 0.0465975f;

    private Rigidbody rb;

    public BoxCollider Rotor1BC;
    public BoxCollider Rotor2BC;
    public BoxCollider Rotor3BC;
    public BoxCollider Rotor4BC;

    public Shader cellShading;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Start") != 0)
        {
            SceneManager.LoadScene(0);
        }

        throttle = Input.GetAxis("Throttle") * -1;
        yaw = Input.GetAxis("Yaw");
        roll = Input.GetAxis("Roll");
        pitch = Input.GetAxis("Pitch");

        // Throttle
        rb.AddForce(rb.transform.up * throttle * power * Time.deltaTime);

        // Yaw
        rb.AddTorque(rb.transform.up * yaw * yawTorque * power * Time.deltaTime);

        // Pitch and Roll
        Vector3 Rotor1Center = Rotor1BC.bounds.center;
        Vector3 Rotor2Center = Rotor2BC.bounds.center;
        Vector3 Rotor3Center = Rotor3BC.bounds.center;
        Vector3 Rotor4Center = Rotor4BC.bounds.center;

        float Rotor1Throttle = (roll + pitch);
        float Rotor2Throttle = (-roll + pitch);
        float Rotor3Throttle = (-roll - pitch);
        float Rotor4Throttle = (roll - pitch);

        rb.AddForceAtPosition(transform.up * Rotor1Throttle * pitchRollTorque * power * Time.deltaTime, Rotor1Center);
        rb.AddForceAtPosition(transform.up * Rotor2Throttle * pitchRollTorque * power * Time.deltaTime, Rotor2Center);
        rb.AddForceAtPosition(transform.up * Rotor3Throttle * pitchRollTorque * power * Time.deltaTime, Rotor3Center);
        rb.AddForceAtPosition(transform.up * Rotor4Throttle * pitchRollTorque * power * Time.deltaTime, Rotor4Center);

        stopRotation();
    }

    public void stopRotation()
    {
        /*
        float yawAngularVelocity = rb.angularVelocity.y;
        float pitchAngularVelocity = rb.angularVelocity.x;
        float rollAngularVelocity = rb.angularVelocity.z;       
        rb.angularVelocity = new Vector3(roll, yaw, pitch);
        */

        float stabilization = stability * (1 + Time.deltaTime);

        float xAngularVelocity = rb.angularVelocity.x;
        yAngularVelocity = rb.angularVelocity.y;
        float zAngularVelocity = rb.angularVelocity.z;


        if (pitch == 0)
        {
            xAngularVelocity = rb.angularVelocity.x * stabilization;
            if (xAngularVelocity > -0.1 && xAngularVelocity < 0.1)
            {
                xAngularVelocity = 0;
            }
        }


        if (yaw == 0)
        {
            yAngularVelocity = rb.angularVelocity.y * stabilization;
            if (yAngularVelocity > -0.1 && yAngularVelocity < 0.1)
            {
                yAngularVelocity = 0;
            }
        }


        if (roll == 0)
        {
            zAngularVelocity = rb.angularVelocity.z * stabilization;
            if (zAngularVelocity > -0.1 && zAngularVelocity < 0.1)
            {
                zAngularVelocity = 0;
            }
        }


        rb.angularVelocity = new Vector3(xAngularVelocity, yAngularVelocity, zAngularVelocity);

    }
}


