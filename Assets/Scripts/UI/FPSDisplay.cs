using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour {
    public TextMeshProUGUI fpstext;

    private float pollingTime = 1f;
    private float time;
    private int frameCount;


    void Update() {
        time += Time.deltaTime;
        frameCount++;

        if(time >= pollingTime) {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            fpstext.text = frameRate.ToString();

            time -= pollingTime;
            frameCount = 0;
        }
    }

}
