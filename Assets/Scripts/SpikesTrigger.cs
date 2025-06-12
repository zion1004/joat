using UnityEngine;

public class SpikesTrigger : MonoBehaviour
{
    public float spikeCooldown;

    public int hitDamage;
    public float hitDotTime;

    private float timer;
    private float hitCooldown;

    void Start()
    {
        timer = -1;
        hitCooldown = -1;
    }

    void Update()
    {
        if(Time.time - timer > spikeCooldown)
        {
            timer = Time.time;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            if (Time.time - hitCooldown > hitDotTime)
            {
                GameManager.Instance.durability -= hitDamage;
                hitCooldown = Time.time;
            }
        }
    }

}
