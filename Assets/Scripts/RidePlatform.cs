using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RidePlatform : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.TryGetComponent(out PlatformMotionTracker platform)) {
            GameManager.Instance.player.rb.interpolation = RigidbodyInterpolation.None;
            transform.SetParent(platform.transform);
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.TryGetComponent(out PlatformMotionTracker platform)) {
            GameManager.Instance.player.rb.interpolation = RigidbodyInterpolation.Interpolate;
            transform.SetParent(null);
        }
    }

}
