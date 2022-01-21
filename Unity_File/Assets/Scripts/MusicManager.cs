using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManager : MonoBehaviour
{
    public class AudioBuffer
    {
        public string name;
        public float time;
    }

    static MusicManager s_Instance;
    public static MusicManager Instance => s_Instance;

    public AudioSource BgmAudioSource;
    public AudioSource sfxAudioSource;
    public AudioClip happinessStateClip;
    public AudioClip cleaningStateStartClip;
    public AudioClip cleaningStateClip;
    public AudioClip brake;
    public AudioClip clean;

    public float bufferTime;
    public List<AudioBuffer> audioBuffers = new List<AudioBuffer>();


    void Awake()
    {
        if (s_Instance != null)
        {
            Destroy(this);
            return;
        }

        s_Instance = this;
    }

    private void Update()
    {
        if(audioBuffers.Capacity > 0)
        {
            foreach (AudioBuffer b in audioBuffers)
            {
                b.time -= Time.deltaTime;
            }
        }
    }

    public void PlayHappinessStateClip()
    {
        BgmAudioSource.clip = happinessStateClip;
        BgmAudioSource.Play();
    }

    public IEnumerator PlayCleaningStateClip()
    {
        BgmAudioSource.volume = 0.8f;
        BgmAudioSource.clip = cleaningStateStartClip;
        BgmAudioSource.Play();

        yield return new WaitForSeconds(cleaningStateStartClip.length);

        BgmAudioSource.volume = 0.25f;
        BgmAudioSource.clip = cleaningStateClip;
        BgmAudioSource.Play();
        UI_MoodBar.Instance.SetActive(false);
        UI_CleanBar.Instance.SetActive(true);
    }

    public void PlayBrake()
    {
        sfxAudioSource.PlayOneShot(brake);
    }    
    
    public void PlayClean()
    {
        sfxAudioSource.PlayOneShot(clean);
    }

    public void PlayClip(AudioClip clip)
    {
        if(audioBuffers.Capacity > 0)
        {
            foreach(AudioBuffer b in audioBuffers)
            {
                if(clip.name == b.name && b.time > 0)
                {
                    return;
                }
                else if(clip.name == b.name)
                {
                    b.time = bufferTime;
                }
            }
        }

        sfxAudioSource.PlayOneShot(clip);

        AudioBuffer buffer = new AudioBuffer();
        buffer.name = clip.name;
        buffer.time = bufferTime;
        audioBuffers.Add(buffer);
    }
}
