using UnityEngine;

public class BgmTrigger : MonoBehaviour
{
    public int tid;
    public AudioManager am;

    void Start()
    {
        am = AudioManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            if (tid == 1)
            {
                AudioManager.Instance.PlayMusic(am.stage1);
            }
            if (tid == 2)
            {
                AudioManager.Instance.PlayMusic(am.stage2);
            }
            if (tid == 3)
            {
                AudioManager.Instance.PlayMusic(am.stage3);
            }
            if (tid == 4)
            {
                AudioManager.Instance.PlayMusic(am.stage4);
            }
            if (tid == 5)
            {
                AudioManager.Instance.PlayMusic(am.stage5);
            }
            
        }
    }
}

