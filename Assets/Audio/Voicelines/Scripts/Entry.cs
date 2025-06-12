using UnityEngine;

public class Entry : MonoBehaviour
{
    public int stage;
    AudioManager am;
    GameManager gm;

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
                if (!gm.stage2entry)
                {
                    gm.stage2entry = true;
                    am.PlayVoice(am.stage2);
                }
                else if (gm.stage2reentry)
                {
                    gm.stage2reentry = false;
                    am.PlayVoice(am.reentryAudio[Random.Range(0, am.reentryAudio.Length)]);
                }
            }
            else if (stage == 3) {
                if (!gm.stage3entry)
                {
                    gm.stage3entry = true;
                    am.PlayVoice(am.stage3);
                }
                else if (gm.stage3reentry)
                {
                    gm.stage3reentry = false;
                    am.PlayVoice(am.reentryAudio[Random.Range(0, am.reentryAudio.Length)]);
                }
            }
            else if (stage == 4)
            {
                if (!gm.stage4entry)
                {
                    gm.stage4entry = true;
                    am.PlayVoice(am.stage4);
                }
                else if (gm.stage4reentry)
                {
                    gm.stage4reentry = false;
                    am.PlayVoice(am.reentryAudio[Random.Range(0, am.reentryAudio.Length)]);
                }
            }
            else if (stage == 5)
            {
                if (!gm.stage5entry)
                {
                    gm.stage5entry = true;
                    am.PlayVoice(am.stage5);
                }
                else if (gm.stage5reentry)
                {
                    gm.stage5reentry = false;
                    am.PlayVoice(am.reentryAudio[Random.Range(0, am.reentryAudio.Length)]);
                }
            }
        }
    }
}