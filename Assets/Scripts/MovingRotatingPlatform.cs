using UnityEngine;

public class MovingRotatingPlatform : MonoBehaviour {
    [Header("Movement Settings")]
    public Vector3 pointA;
    public Vector3 pointB;
    public float moveSpeed = 1f;

    [Header("Pause Settings")]
    public float pauseDuration = 1.5f;

    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.up; 
    public float rotationSpeed = 0f;
    public bool pauseRotation = true; // Whether to pause rotation every 360 degrees
    public float rotationPauseDuration = 1f; // How long to pause rotation

    public GameObject siba;

    private Vector3 startPos;
    private float t;
    private Quaternion startRot;

    private bool movingToB = true; 
    private float pauseTimer = 0f; 
    private bool isPaused = false;

    private float rotationAngle = 0f;
    private float rotationPauseTimer = 0f;
    private bool isRotationPaused = false;

    void Start() {

        startPos = transform.position;
        startRot = transform.rotation;
    }

    void FixedUpdate() {
        //transform.Rotate(rotationAxis, rotationSpeed * Time.fixedDeltaTime);

        //t += Time.fixedDeltaTime * moveSpeed;
        //Vector3 newPos = Vector3.Lerp(pointA, pointB, Mathf.PingPong(t, 1f));

        HandleRotation();
        HandleMovement();

    }

    private void HandleRotation() {
        // Skip rotation if we're paused
        if(isRotationPaused) {
            rotationPauseTimer += Time.fixedDeltaTime;


            if(rotationPauseTimer >= rotationPauseDuration) {
                isRotationPaused = false;
                rotationPauseTimer = 0f;
            }
            return;
        }


        float rotationAmount = rotationSpeed * Time.fixedDeltaTime;

        transform.Rotate(rotationAxis, rotationAmount);


        if(pauseRotation) {
            rotationAngle += Mathf.Abs(rotationAmount);

            if(rotationAngle >= 360f) {
                rotationAngle = 0;
                isRotationPaused = true; 
            }
        }
    }


    private void HandleMovement() {

        if(isPaused) {
            pauseTimer += Time.fixedDeltaTime;

            if(pauseTimer >= pauseDuration) {
                isPaused = false;
                pauseTimer = 0f;
            }
            return;
        }

        t += Time.fixedDeltaTime * moveSpeed;
        float normalizedT = movingToB ? t : 1 - t;


        if(normalizedT >= 1f) {

            normalizedT = 1f;
            t = 0f;
            movingToB = false;
            isPaused = true;
            transform.position = pointB;
        }
        else if(normalizedT <= 0f) {
            // We've reached point A
            normalizedT = 0f;
            t = 0f;
            movingToB = true;
            isPaused = true;
            transform.position = pointA;
        }
        else {
            // Normal movement
            Vector3 newPos = Vector3.Lerp(pointA, pointB, normalizedT);
            transform.position = newPos;
        }
    }
}
