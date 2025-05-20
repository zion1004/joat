using UnityEngine;
using UnityEngine.EventSystems;

public class ActualTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public GameObject gmo_toolTip;
    public bool isActive;
    void Start() {
        // I added this in case I forgot to set the tooltip object
        if(gmo_toolTip != null) {
            gmo_toolTip.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        // Same here
        if(gmo_toolTip != null && isActive) {
            gmo_toolTip.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        // and same here
        if(gmo_toolTip != null && isActive) {
            gmo_toolTip.SetActive(false);
        }
    }
}