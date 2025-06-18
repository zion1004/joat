using UnityEngine;
using System.Collections;

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


    public AudioClip stage1bgm;
    public AudioClip stage2bgm;
    public AudioClip stage3bgm;
    public AudioClip stage4bgm;
    public AudioClip stage5bgm;
    public AudioClip anvil;
    public AudioClip forge;
    public AudioClip btnClick;




    public float fadeDuration = 2f;

    public Coroutine fadeCoroutine;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(AudioClip clip)
    {
        EffectsSource.Stop();
        EffectsSource.clip = clip;
        EffectsSource.Play();
    }

    public AudioClip GetStageMusic(int i)
    {
        if (i == 2)
        {
            return stage2bgm;
        }
        else if (i == 3)
        {
            return stage3bgm;
        }
        else if (i == 4)
        {
            return stage4bgm;
        }
        else if (i == 5)
        {
            return stage5bgm;
        }
        return stage1bgm;
    }
    public void PlayMusic(AudioClip clip)
    {
        if (fadeCoroutine == null)
        {
            fadeCoroutine = StartCoroutine(FadeAudio(clip));
            return;
        }
        StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeAudio(clip));
    }

    private IEnumerator FadeAudio(AudioClip clip)
    {
        float start = Time.time;
        while (Time.time - start < fadeDuration)
        {
            float normalizedTime = (Time.time - start) / fadeDuration;
            MusicSource.volume = Mathf.Lerp(MusicSource.volume, 0f, normalizedTime);
            yield return null;
        }
        MusicSource.Stop();
        MusicSource.clip = clip;
        MusicSource.loop = true;
        MusicSource.Play();
        start = Time.time;
        while (Time.time - start < fadeDuration)
        {
            float normalizedTime = (Time.time - start) / fadeDuration;
            MusicSource.volume = Mathf.Lerp(0f, 1f, normalizedTime);
            yield return null;
        }
        fadeCoroutine = null;
    }

    public void PlayVoice(AudioClip clip)
    {
        VoiceSource.Stop();
        VoiceSource.clip = clip;
        VoiceSource.Play();
    }
}