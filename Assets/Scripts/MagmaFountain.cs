using System.Collections.Generic;
using UnityEngine;

public class MagmaFountain : MonoBehaviour
{

    public ParticleSystem magmaParticle;
    List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
    public float magmaCooldown;

    public int hitDamage;
    public float hitDotTime;

    private float timer;
    private float hitCooldown;

    private void OnParticleTrigger()
    {
        if (Time.time - hitCooldown > hitDotTime)
        {
            GameManager.Instance.durability -= hitDamage;
            hitCooldown = Time.time;
        }
    }
    void Start()
    {
        timer = -1;
        hitCooldown = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - timer > magmaCooldown)
        {
            if (magmaParticle.isStopped)
            {
                timer = Time.time;
                magmaParticle.Play();
            }
        }
    }
}
