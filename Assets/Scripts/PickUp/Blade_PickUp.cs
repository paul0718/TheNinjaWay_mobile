using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade_PickUp : MonoBehaviour
{
    AudioSource _audioSource; // NEED TO ADD "AUDIO SOURCE" COMPONENT TO THE BUTTON
    public AudioClip pickSnd;

    void Start () {
        _audioSource = GetComponent<AudioSource>();
        pickSnd = (AudioClip)Resources.Load("Audio/pickSnd"); // add file "pickSnd" to Resources/Audio
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            _audioSource.PlayOneShot(pickSnd);
            Destroy(gameObject);
            PublicVars.blade_active = true;
        }
    }
}

/*
for a sound to work:
    AudioSource _audioSource;
    public AudioClip yourSnd;

    Start:
    _audioSource = GetComponent<AudioSource>();
    yourSnd = (AudioClip)Resources.Load("yourSnd file dir under folder Resources");

    When play:
    _audioSource.PlayOneShot(yourSnd);
*/
