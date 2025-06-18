using UnityEngine;

public class FallJorong : MonoBehaviour
{
    AudioManager am;

    public int stage;
    private bool fell;
    private float fallTime;

    void Start()
    {
        am = AudioManager.Instance;
        fallTime = -1f;
    }

    private void Update()
    {
        if (fell){
            if (Time.time - fallTime >= 3f) {
                fell = false;
                am.PlayVoice(am.fallAudio[Random.Range(0, am.fallAudio.Length)]);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            fallTime = Time.time;
            fell = true;
            GameManager.Instance.currentStage = stage;
            if (stage == 2)
            {
                GameManager.Instance.stage2reentry = true;
            }
            else if (stage == 3)
            {
                GameManager.Instance.stage3reentry = true;
            }
            else if (stage == 4)
            {
                GameManager.Instance.stage4reentry = true;
            }
            else if (stage == 5)
            {
                GameManager.Instance.stage5reentry = true;
            }
        }
    }
}
