using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{

    public float power = 4000;
    public float yawTorque = 0.01f;
    public float pitchRollTorque = 0.01f;
    public float stability = 0.95f;

    private float throttle;
    private float yaw;
    private float pitch;
    private float roll;

    private float targetVolume;
    public float dVolumeChangeSpeed = 0.1f;

    private AudioSource audio;
    private Rigidbody rb;
    public BoxCollider Rotor1BC;
    public BoxCollider Rotor2BC;
    public BoxCollider Rotor3BC;
    public BoxCollider Rotor4BC;

    public GameObject Blade1;
    public GameObject Blade2;
    public GameObject Blade3;
    public GameObject Blade4;

    public Shader cellShading;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
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

        //Audio

        float baseVolume = throttle * throttle * 0.2f;
        float volumeExtra = (Mathf.Abs(yaw) + Mathf.Abs(roll) + Mathf.Abs(pitch)) * 0.15f;
        targetVolume = baseVolume + volumeExtra;
        audio.volume = Mathf.Lerp(audio.volume, targetVolume, Time.deltaTime * 10);
        audio.pitch = audio.volume + 0.65f;

        int direction = 1;
            if (throttle <0)
        {
            direction = -1;
        }

        //Blade rotation
        Blade1.transform.RotateAround(Blade1.transform.position, Blade1.transform.up, audio.volume * 10000 * Time.deltaTime * direction);
        Blade2.transform.RotateAround(Blade2.transform.position, Blade2.transform.up, audio.volume * 10000 * Time.deltaTime * direction);
        Blade3.transform.RotateAround(Blade3.transform.position, Blade3.transform.up, audio.volume * -10000 * Time.deltaTime * direction);
        Blade4.transform.RotateAround(Blade4.transform.position, Blade4.transform.up, audio.volume * -10000 * Time.deltaTime * direction);
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
        float yAngularVelocity = rb.angularVelocity.y;
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


