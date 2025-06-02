using UnityEngine;

public class Altar : MonoBehaviour
{
    private float altartime = 3f;
    private float stayedtime = 0f;
    public Ending ending;
    private bool notEnding = true;

    void OnTriggerEnter(Collider other)
    {
        stayedtime = Time.time;
    }


    void OnTriggerStay(Collider other)
    {
        if (notEnding)
        {
            if (Time.time - stayedtime >= altartime)
            {
                ending.gameObject.SetActive(true);
                ending.StartEndingScene();
                notEnding = false;
            }
        }
    }
}
