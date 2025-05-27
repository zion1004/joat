using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public AudioClip bgm;
    void Start()
    {
        AudioManager.Instance.PlayMusic(bgm);
    }
}
