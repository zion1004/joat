using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource EffectsSource;
    public AudioSource MusicSource;

    public static AudioManager Instance = null;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else if(Instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(AudioClip clip) {
        EffectsSource.Stop();
        EffectsSource.clip = clip;
        EffectsSource.Play();
    }

    public void PlayMusic(AudioClip clip) {
        MusicSource.Stop();
        MusicSource.clip = clip;
        MusicSource.loop = true;
        MusicSource.Play();
    }
}
