using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public Rigidbody droneRB;
    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public Transform m_BombTransform;
    public AudioSource m_ShootingAudio;
    public AudioClip m_ChargingClip;
    public AudioClip m_FireClip;
    public AudioClip m_BombClip;
    public float firePower = 15f;

    public int m_PlayerNumber;
    public float maxHeat;
    public float heatDissipation;
    public float spread;
    public float heat;

    private string fireButton1;
    private string fireButton2;

    private void Start()
    {
        fireButton1 = "Fire" + m_PlayerNumber + "A";
        fireButton2 = "Fire" + m_PlayerNumber + "B";
    }

    private void Update()
    {

        if (heat > 0)
        {
            heat = heat - Time.deltaTime * heatDissipation;
        }
        else
        {
            heat = 0;
        }


        if (Input.GetButtonDown(fireButton1) || Input.GetButtonDown(fireButton2))
        {

            if (heat < maxHeat)
            {
                Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
                shellInstance.velocity = firePower * droneRB.transform.forward + new Vector3(Random.Range(-heat * spread, heat * spread), Random.Range(-heat * spread, heat * spread), Random.Range(-heat * spread, heat * spread));
                m_ShootingAudio.clip = m_FireClip;
                m_ShootingAudio.Play();
                heat += 5;
            }
        }
    }
}