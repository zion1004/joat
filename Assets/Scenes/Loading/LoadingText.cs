using TMPro;
using UnityEngine;

public class LoadingText : MonoBehaviour
{
    public TextMeshProUGUI text;
    private float timer;
    private int dot;
    void Start()
    {
        timer = -1f;
        dot = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timer >= 0.25f)
        {
            timer = Time.time;
            if (dot == 1)
            {
                text.text = "Loading.";
                dot++;
            }
            else if (dot == 2)
            {
                text.text = "Loading..";
                dot++;
            }
            else
            {
                text.text = "Loading...";
                dot = 1;
            }
        }
    }
}
