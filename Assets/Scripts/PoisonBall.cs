using UnityEngine;

public class PoisonBall : MonoBehaviour
{
    public GameObject ball;
    public ParticleSystem particle;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            Debug.Log("무야호~");
        }
        else
        {
            Debug.Log("연식쿤~");
        }
    }
}
