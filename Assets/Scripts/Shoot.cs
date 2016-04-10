using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public AudioSource m_ShootingAudio;
    public AudioClip m_ChargingClip;
    public AudioClip m_FireClip;

    public float firePower = 15f;

    private string fireButton1;
    private string fireButton2;


    private void Start()
    {
        fireButton1 = "Fire" + m_PlayerNumber + "A";
        fireButton2 = "Fire" + m_PlayerNumber + "B";
    }

    private void Update()
    {
        if (Input.GetButtonDown(fireButton1) || Input.GetButtonDown(fireButton2))
        {
            Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
            shellInstance.velocity = firePower * m_FireTransform.forward;
            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play();
        }
    }

}