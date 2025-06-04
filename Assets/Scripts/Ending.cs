using UnityEngine;

public class Ending : MonoBehaviour
{
    public GameObject wingPrefab;
    public GameObject lightPrefab;
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
    private bool startEnd = false;
    private float startEndTime = 0f;

    private Vector3 wingPos = new Vector3(276, 360, 0.4f);
    private GameObject wing;
    private Material wingMaterial;
    private Vector3 lightPos = new Vector3(276, 363, -2);
    private GameObject lightParent;
    private Light spotlight;

    private bool startFly;
    private float startFlyTime;
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
        if (ending && !startEnd)
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
                if (!startEnd && smoothRotTime >= 1)
                {
                    startEnd = true;
                    startEndTime = Time.time;
                    wing = Instantiate(wingPrefab);
                    lightParent = Instantiate(lightPrefab);
                    wingMaterial = wing.GetComponent<MeshRenderer>().material;
                    spotlight = lightParent.transform.GetChild(0).GetComponent<Light>();
                    wing.transform.SetParent(GameManager.Instance.player.transform);
                    wingMaterial.SetFloat("_Metallic", 0f);
                    wingMaterial.SetFloat("_Smoothness", 0f);
                    spotlight.spotAngle = 0;
                    spotlight.intensity = 0;
                }
            }
        }
        else if (startEnd && !startFly)
        {
            var elapsedEndTime = Mathf.Clamp((Time.time - startEndTime) / 1, 0f, 1f);
            var smoothEndTime = Mathf.SmoothStep(0f, 1f, elapsedEndTime);
            var newColor = Mathf.Lerp(0f, 1f, smoothEndTime);
            var newAngle = Mathf.Lerp(0f, 50f, smoothEndTime);
            var newIntensity = Mathf.Lerp(0, 150, smoothEndTime);
            wingMaterial.SetFloat("_Metallic", newColor);
            wingMaterial.SetFloat("_Smoothness", newColor);
            spotlight.spotAngle = newAngle;
            spotlight.intensity = newIntensity;

            if (!startFly && smoothEndTime >= 1)
            {
                startFly = true;
                startFlyTime = Time.time;
                currentPos = player.transform.position;
                endPos = new Vector3(276, 500, 0);
                GameManager.Instance.mainCamera.GetComponent<CameraScript>().enabled = false;
            }
        }
        else if (startFly)
        {
            var elapsedTime = Mathf.Clamp((Time.time - startFlyTime) / 10f, 0f, 1f);
            var smoothTime = Mathf.SmoothStep(0f, 1f, elapsedTime);
            var newPos = Vector3.Lerp(currentPos, endPos, smoothTime);
            player.transform.position = newPos;

        }

    }
}
