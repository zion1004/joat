using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] float smoothSpeed = 2.5f; 
    [SerializeField] Vector3 offset = new Vector3(0, 2, -20); 

    void LateUpdate()
    {
        if(GameManager.Instance.playerTransform == null){
            return;
        }
        Vector3 desiredPosition = GameManager.Instance.playerTransform.position + offset;  
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
