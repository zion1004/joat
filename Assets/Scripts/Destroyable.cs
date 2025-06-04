using UnityEngine;
using System.Collections;
using System.Linq;

public class Destroyable : MonoBehaviour {
    public string itemID;

    [SerializeField] Transform upperPart;
    [SerializeField] Transform lowerPart;
    [SerializeField] float rotationDuration = 1f;
    [SerializeField] float objectDuration = 2f;

    [SerializeField] float upperAngle = 0f;
    [SerializeField] float lowerAngle = 0f;

    [SerializeField] int defense = 50;
    [SerializeField] int maxDefense = 50;


    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] MeshCollider meshCollider;

    public Material outlineMaterial;

    private Material outlineMaterialInstance;

    public float tiltAmount = 10f;
    public float tiltDuration = 0.2f; 
    public float resetSpeed = 5f;
    private Coroutine tiltCoroutine;
    private Quaternion originalRotation;

    private void Awake() {
        if(string.IsNullOrEmpty(itemID)) {
            itemID = gameObject.name;
        }
    }

    private void Start() {
        if(GameManager.Instance.IsDestroyed(itemID)) {
            gameObject.SetActive(false);
        }
        else {
            Material[] temp = meshRenderer.materials;
            Material[] newMats = new Material[temp.Length + 1];
            int i = 0;
            for(; i < temp.Length; i++) {
                newMats[i] = temp[i];
            }
            outlineMaterialInstance = Instantiate(outlineMaterial);
            newMats[i] = outlineMaterialInstance;
            meshRenderer.materials = newMats;
        }
    }

    public void GetSliced(int attack, Vector3 hitDirection) {
        float a = (float)defense / (float)maxDefense;
        UpdateHealth(a);

        if(attack < defense) {
            TiltOnHit(hitDirection);
            defense -= attack;
        }
        else {
            GameManager.Instance.MarkDestroyed(itemID);
            meshRenderer.enabled = false;
            meshCollider.enabled = false;

            upperPart.gameObject.SetActive(true);
            MeshCollider upperCollider = upperPart.GetComponent<MeshCollider>();

            lowerPart.gameObject.SetActive(true);
            MeshCollider lowerCollider = lowerPart.GetComponent<MeshCollider>();

            StartCoroutine(RotateOverTime(upperPart, new Vector3(upperAngle, 0, 0), rotationDuration));
            StartCoroutine(RotateOverTime(lowerPart, new Vector3(-lowerAngle, 0, 0), rotationDuration));
        }
    }

    void UpdateHealth(float health) {
        outlineMaterialInstance.SetFloat("_HealthRatio", health);
    }

    public void TiltOnHit(Vector3 hitDirection) {
        if(tiltCoroutine != null) {
            StopCoroutine(tiltCoroutine); 
            transform.rotation = originalRotation; 
        }

        tiltCoroutine = StartCoroutine(Tilt(hitDirection));
    }

    private IEnumerator Tilt(Vector3 hitDirection) {
        originalRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(hitDirection * tiltAmount) * originalRotation;
        float elapsedTime = 0f;

        while(elapsedTime < tiltDuration) {
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, elapsedTime / tiltDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;

        while(Quaternion.Angle(transform.rotation, originalRotation) > 0.1f) {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, Time.deltaTime * resetSpeed);
            yield return null;
        }

        transform.rotation = originalRotation; 
        tiltCoroutine = null;
    }

    private IEnumerator RotateOverTime(Transform part, Vector3 targetRotation, float duration) {
        if(part == null) yield break;

        Quaternion startRotation = part.rotation;
        Quaternion endRotation = Quaternion.Euler(part.eulerAngles + targetRotation);
        float elapsedTime = 0f;

        while(elapsedTime < duration) {
            part.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        part.rotation = endRotation;
        Destroy(part.gameObject, objectDuration);
    }
}
