using UnityEngine;

public class BgmTrigger : MonoBehaviour
{
    public int tid;
    private AudioManager am;

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
                AudioManager.Instance.PlayMusic(am.stage1bgm);
            }
            if (tid == 2)
            {
                AudioManager.Instance.PlayMusic(am.stage2bgm);
            }
            if (tid == 3)
            {
                AudioManager.Instance.PlayMusic(am.stage3bgm);
            }
            if (tid == 4)
            {
                AudioManager.Instance.PlayMusic(am.stage4bgm);
            }
            if (tid == 5)
            {
                AudioManager.Instance.PlayMusic(am.stage5bgm);
            }
            
        }
    }
}

