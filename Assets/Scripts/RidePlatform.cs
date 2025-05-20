using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RidePlatform : MonoBehaviour {
    private PlatformMotionTracker currentPlatform;

    void Start() {
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.TryGetComponent(out PlatformMotionTracker platform)) {
            transform.SetParent(platform.transform);
        }
    }

    void OnCollisionExit(Collision other) {
        if(other.gameObject.TryGetComponent(out PlatformMotionTracker platform)) {
            transform.SetParent(null);
        }
    }


    void OnTriggerEnter(Collider other) {
        if(other.gameObject.TryGetComponent(out WheelMotionTracker platform)) {
            transform.SetParent(platform.transform);
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.TryGetComponent(out WheelMotionTracker platform)) {
            transform.SetParent(null);
        }
    }

}
