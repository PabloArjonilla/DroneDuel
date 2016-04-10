﻿using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
 
    public ParticleSystem m_ExplosionParticles;
    public AudioSource m_ExplosionAudio;
    public float m_MaxDamage = 100f;
    public float m_ExplosionForce = 1000f;
    public float m_MaxLifeTime = 10f;
    public float m_ExplosionRadius = 5f;

    private Rigidbody rb;
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
        // Find all the tanks in an area around the shell and damage them.
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidBody = colliders[i].GetComponent<Rigidbody>();

            if (!targetRigidBody)
                continue;

            targetRigidBody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);
      
        }

        m_ExplosionParticles.transform.parent = null;
        m_ExplosionParticles.Play();

        m_ExplosionAudio.Play();
        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
        Destroy(gameObject);
    }

}