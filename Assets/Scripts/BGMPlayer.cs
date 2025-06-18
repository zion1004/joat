using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.GetStageMusic(GameManager.Instance.currentStage));
    }
}
