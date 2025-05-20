using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ClampUIToAspectRatio : MonoBehaviour {
    [Tooltip("The target aspect ratio (e.g., 16:9 = 1.777...)")]
    public float targetAspectRatio = 16f / 9f;

    private RectTransform rectTransform;

    void Start() {
        rectTransform = GetComponent<RectTransform>();
        UpdateUIArea();
    }

    void Update() {
        // Update every frame in case screen res changes
        UpdateUIArea();
    }

    void UpdateUIArea() {
        float currentAspect = (float)Screen.width / Screen.height;
        float scaleFactor = 1f;

        if(currentAspect > targetAspectRatio) {
            // Screen is wider than 16:9 ¡æ pillarbox
            scaleFactor = targetAspectRatio / currentAspect;
            rectTransform.anchorMin = new Vector2((1f - scaleFactor) / 2f, 0f);
            rectTransform.anchorMax = new Vector2(1f - (1f - scaleFactor) / 2f, 1f);
        }
        else {
            // Screen is taller than 16:9 ¡æ letterbox
            scaleFactor = currentAspect / targetAspectRatio;
            rectTransform.anchorMin = new Vector2(0f, (1f - scaleFactor) / 2f);
            rectTransform.anchorMax = new Vector2(1f, 1f - (1f - scaleFactor) / 2f);
        }

        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }
}
