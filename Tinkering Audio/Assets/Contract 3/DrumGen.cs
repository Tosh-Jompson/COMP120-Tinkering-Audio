using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumGen : MonoBehaviour
{
    public AudioSource audioSource;

    public bool playing;

    public float startPitch = 1f;
    public float pitch;
    public float pitch2;
    public float pitch3;
    public float pitch4;

    // Calls the PitchRandomiser function and retrieves the AudioSource
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PitchRandomiser();
    }

    // Loops the DrumStruct Enum without it overlaping
    void Update()
    {
        if (playing == false)
        {
            playing = true;
            StartCoroutine(DrumStruct(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)));
        }
    }

    // Creates a series of randomised pitchs
    public void PitchRandomiser()
    {
        pitch = Random.Range(0.1f, 2f);
        pitch2 = Random.Range(0.1f, 2f);
        pitch3 = Random.Range(0.1f, 2f);
        pitch4 = Random.Range(0.1f, 2f);
    }

    // Handles timing and pitch changes of drum notes
    IEnumerator DrumStruct(float delay, float delay2, float delay3, float delay4)
    {
        yield return new WaitForSeconds(delay);
        audioSource.pitch = pitch;
        yield return new WaitForSeconds(delay2);
        audioSource.pitch = pitch2;
        yield return new WaitForSeconds(delay3);
        audioSource.pitch = pitch3;
        yield return new WaitForSeconds(delay4);
        audioSource.pitch = pitch4;
        playing = false;
    }
}
