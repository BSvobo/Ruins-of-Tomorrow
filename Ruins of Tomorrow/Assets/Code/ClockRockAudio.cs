using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockRockAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip soundEffect;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClockRockChange()
    {
        audioSource.PlayOneShot(soundEffect, .9f);
    }
}
