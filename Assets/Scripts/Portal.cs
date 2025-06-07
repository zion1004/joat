using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] public GameManager.BlacksmithPosition portalID;          
    [SerializeField] public string sceneToLoad = "Blacksmith";
    
    private bool isPlayerInRange;

    private void Update() {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.UpArrow)) {
            GameManager.Instance.returnPosition = GameManager.Instance.player.transform.position;
            GameManager.Instance.currentBlacksmith = portalID;
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 10){         
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.layer == 10) {
            isPlayerInRange = false;
        }
    }
}
