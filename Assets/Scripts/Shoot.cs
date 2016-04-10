using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public Transform m_BombTransform;
    public AudioSource m_ShootingAudio;
    public AudioClip m_ChargingClip;
    public AudioClip m_FireClip;
    public AudioClip m_BombClip;
    public BoxCollider Rotor1BC;
    public BoxCollider Rotor2BC;
    public float firePower = 15f;
    public Camera camera;


    private Vector3 originalPos;

    public Rigidbody rb;
    private string fireButton1;
    private string fireButton2;
    private string bombButton1;
    private string bombButton2;

    private bool once = true;

    private void Start()
    {

        originalPos = camera.transform.localPosition;
        rb = GetComponent<Rigidbody>();
        fireButton1 = "Fire" + m_PlayerNumber + "A";
        fireButton2 = "Fire" + m_PlayerNumber + "B";
    }

    private void Update()
    {


        if (Input.GetButtonDown(fireButton1) || Input.GetButtonDown(fireButton2))
        {
            Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
            rb.AddForce(transform.forward * firePower * -10f);

            // Vector3 Rotor1Center = Rotor1BC.bounds.center;
            //  Vector3 Rotor2Center = Rotor2BC.bounds.center;
            // rb.AddForceAtPosition(transform.up * 5f, Rotor1Center);
            //  rb.AddForceAtPosition(transform.up * 5f, Rotor2Center);
            shellInstance.velocity = firePower * m_FireTransform.forward;
            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play();
        }


    }

}