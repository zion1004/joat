using UnityEngine;

public class ReentryJorong : MonoBehaviour
{
    public int stage;
    AudioManager am;
    GameManager gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        am = AudioManager.Instance;
        gm = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            if (stage == 2)
            {
                if (gm.stage2reentry)
                {
                    gm.stage2reentry = false;
                    am.PlayVoice(am.reentryAudio[Random.Range(0, am.reentryAudio.Length)]);
                }
            }
            else if (stage == 3) {
                if (gm.stage3reentry)
                {
                    gm.stage3reentry = false;
                    am.PlayVoice(am.reentryAudio[Random.Range(0, am.reentryAudio.Length)]);
                }
            }
            else if (stage == 4)
            {
                if (gm.stage4reentry)
                {
                    gm.stage4reentry = false;
                    am.PlayVoice(am.reentryAudio[Random.Range(0, am.reentryAudio.Length)]);
                }
            }
            else if (stage == 5)
            {
                if (gm.stage5reentry)
                {
                    gm.stage5reentry = false;
                    am.PlayVoice(am.reentryAudio[Random.Range(0, am.reentryAudio.Length)]);
                }
            }
        }
    }
}