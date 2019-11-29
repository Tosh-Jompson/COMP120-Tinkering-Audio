using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodySelector : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioEchoFilter echoFilter;

    public AudioClip loop1;
    public AudioClip loop2;
    public AudioClip loop3;

    public float startPitch = 1f;
    public float newPitch;
    public float delay;
    public float time;

    private int clipRand;

    private bool playing;

    // Calls the Randomiser and ClipSelector functions as well as retrieving the AudioSource and AudioEchoFilter
    void Start()
    {
        Randomiser();
        playing = false;
        audioSource = GetComponent<AudioSource>();
        echoFilter = GetComponent<AudioEchoFilter>();
        ClipSelector();
        audioSource.Play();
    }

    // Loops the BreakDown Enum without it overlaping
    void Update()
    {
        if (playing == false)
        {
            playing = true;
            StartCoroutine(BreakDown());
        }
    }

    // Randomises the breakdowns pitch and timing
    public void Randomiser()
    {
        newPitch = Random.Range(0.25f, 2);
        delay = Random.Range(10.0f, 20.0f);
        time = Random.Range(3.0f, 6.0f);
    }

    // Selects a random clarinet loop and adds it to the AudioSource
    public void ClipSelector()
    {
        List<AudioClip> clips = new List<AudioClip>();
        clips.Add(loop1);
        clips.Add(loop2);
        clips.Add(loop3);

        clipRand = Random.Range(0, clips.Count);
        audioSource.clip = clips[clipRand];
    }

    // Handles timing echo and pitch changes throughout the song
    IEnumerator BreakDown()
    {
        yield return new WaitForSeconds(delay);
        audioSource.pitch = newPitch;
        echoFilter.enabled = true;
        yield return new WaitForSeconds(time);
        echoFilter.enabled = false;
        audioSource.pitch = startPitch;
        playing = false;
    }
}
