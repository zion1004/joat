using UnityEngine;

public class PoisonPipe : MonoBehaviour
{
    public GameObject poisonBall;
    public float pimpleTimer;
    public Vector2 ballSpeed;
    private float lastPimpleTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        lastPimpleTime = -1f;
        Physics.IgnoreLayerCollision(17, 16, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastPimpleTime > pimpleTimer)
        {
            lastPimpleTime = Time.time;
            GameObject ball = Instantiate(poisonBall, transform.position, Quaternion.identity);
            ball.GetComponent<Rigidbody>().AddForce(ballSpeed, ForceMode.Impulse);
        }
    }
}
