using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string itemID;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() {
        if(string.IsNullOrEmpty(itemID)) {
            itemID = gameObject.name;
        }
    }
    // Update is called once per frame

    private void Start() {
        if(GameManager.Instance.IsCollected(itemID)) {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 10) {
            GameManager.Instance.MarkCollected(itemID);
            // Play sound/animation/etc.
            gameObject.SetActive(false);
        }
    }
}
