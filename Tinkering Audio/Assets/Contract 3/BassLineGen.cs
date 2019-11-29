using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BassLineGen : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip outAudioClipLow;
    private AudioClip outAudioClipHigh;

    public float delay;
    public float delay2;

    public bool playing;
    
    // Retrieves the AudioSource
    void Start()
    {
        playing = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Loops the BassDelay Enum without it overlaping
    void Update()
    {
        if (playing == false)
        {
            playing = true;
            StartCoroutine(BassDelay());
        }
    }

    // Randomises frequency and delay of bass notes
    public void Randomiser()
    {
        outAudioClipLow = CreateToneAudioClip(Random.Range(50, 60));
        outAudioClipHigh = CreateToneAudioClip(Random.Range(60, 70));
        delay = Random.Range(2.25f, 5f);
        delay2 = Random.Range(2.25f, 5f);
    }

    // Plays lower bass tone
    public void PlayOutAudioLow()
    {
        audioSource.PlayOneShot(outAudioClipLow);
    }

    // Plays higher bass tone
    public void PlayOutAudioHigh()
    {
        audioSource.PlayOneShot(outAudioClipHigh);
    }

    // Handles timing between low and high bass notes
    IEnumerator BassDelay()
    {
        yield return new WaitForSeconds(delay);
        PlayOutAudioLow();
        yield return new WaitForSeconds(delay2);
        PlayOutAudioHigh();
        playing = false;
    }
    
    // Generates and returns an AudioClip of the inputted frequency
    private AudioClip CreateToneAudioClip(int frequency)
    {
        int sampleDurationSecs = 2;
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs;
        float maxValue = 1f / 4f;
        
        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);
        
        float[] samples = new float[sampleLength];

        for (var i = 0; i < sampleLength; i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float) i / (float) sampleRate));
            float v = s * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }
}