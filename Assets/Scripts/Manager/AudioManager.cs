using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource EffectsSource;
    public AudioSource MusicSource;
    public AudioSource VoiceSource;

    public static AudioManager Instance = null;

    public AudioClip[] idleAudio;

    public AudioClip[] quitAudio;

    public AudioClip[] fallAudio;

    public AudioClip[] reentryAudio;


    public AudioClip stage1;
    public AudioClip stage2;
    public AudioClip stage3;
    public AudioClip stage4;
    public AudioClip stage5;
    public AudioClip ending;


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

    public void PlayVoice(AudioClip clip)
    {
        VoiceSource.Stop();
        VoiceSource.clip = clip;
        VoiceSource.Play();
    }
}