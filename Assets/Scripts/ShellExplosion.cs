using System.Collections.Generic;
using UnityEngine;

public class ShellExplosion : MonoBehaviour
{

    public ParticleSystem m_ExplosionParticles;
    public AudioSource m_ExplosionAudio;
    private Rigidbody rb;

    public float m_MaxDamage = 100f;
    public float m_ExplosionForce = 1000f;
    public float m_MaxLifeTime = 10f;
    public float m_ExplosionRadius = 5f;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, m_MaxLifeTime);
    }

    void Update()
    {
        transform.forward = rb.velocity;
    }


    private void OnTriggerEnter(Collider other)
    {
  
        Collider[] collidersMap = Physics.OverlapSphere(transform.position, m_ExplosionRadius);

        for (int i = 0; i < collidersMap.Length; i++)
        {
            Rigidbody targetRigidBody = collidersMap[i].GetComponent<Rigidbody>();

            if (targetRigidBody != null)
            { 
                targetRigidBody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);
                m_ExplosionParticles.transform.parent = null;
                m_ExplosionParticles.Play();
                m_ExplosionAudio.Play();
                Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
                Destroy(gameObject);
            }
        }
    }

}