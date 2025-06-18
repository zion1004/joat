using UnityEngine;

public class MainBgmPlayer : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.stage5bgm);
    }
}
