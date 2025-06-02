using UnityEngine;

public class Ending : MonoBehaviour
{
    private Vector3 endPos = new Vector3(276, 360, 0);
    private float endRot = 0f;
    private float currentRot;
    private Vector3 currentPos;

    private float startTime = 0f;
    private float endTime = 2f;
    private float startRotTime = 0f;
    private float endRotTime = 2f;
    private bool ending = false;
    private bool startRot = false;
    Player player;
    public void StartEndingScene()
    {
        player = GameManager.Instance.player;
        currentPos = player.transform.position;
        currentRot = player.transform.rotation.eulerAngles.z;
        player.rb.useGravity = false;
        player.rb.isKinematic = true;
        ending = true;
        startTime = Time.time;
        endRot = currentRot >= 180f ? 360f : 0f;
    }


    void Update()
    {
        if (ending)
        {
            var elapsedTime = Mathf.Clamp((Time.time - startTime) / endTime, 0f, 1f);
            var smoothTime = Mathf.SmoothStep(0f, 1f, elapsedTime);
            if (!startRot && smoothTime >= 1)
            {
                startRot = true;
                startRotTime = Time.time;
            }
            var newPos = Vector3.Lerp(currentPos, endPos, smoothTime);
            player.transform.position = newPos;
            if (startRot)
            {
                var elapsedRotTime = Mathf.Clamp((Time.time - startRotTime) / endRotTime, 0f, 1f);
                var smoothRotTime = Mathf.SmoothStep(0f, 1f, elapsedRotTime);
                var newRot = Mathf.Lerp(currentRot, endRot, smoothRotTime);
                player.transform.rotation = Quaternion.Euler(0f, 0f, newRot);
            }
        }

    }
}
