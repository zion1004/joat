using UnityEngine;

public class LoadingSword : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, 0, Time.deltaTime * speed);
    }
}
