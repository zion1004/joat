using UnityEngine;
using System.Collections;

public class PoisonBall : MonoBehaviour
{
    public GameObject ball;
    public ParticleSystem particle;
    public Rigidbody rb;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 16)
        {
            rb.linearVelocity = Vector3.zero;
            rb.useGravity = false;
            ball.SetActive(false);
            rb.isKinematic = true;
            particle.gameObject.SetActive(true);
            particle.Play();
            StartCoroutine(Boom());
        }

    }

    private IEnumerator Boom()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
