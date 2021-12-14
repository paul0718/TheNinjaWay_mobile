using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMo_Pickup : MonoBehaviour
{
    AudioSource _audioSource;
    public AudioClip pickSnd;
    void Start() {
        _audioSource = GetComponent<AudioSource>();
        pickSnd = (AudioClip)Resources.Load("Audio/pickSnd"); // add file "pickSnd" to Resources/Audio
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            _audioSource.PlayOneShot(pickSnd);
            Destroy(gameObject);
            PublicVars.has_SlowMo_item = true;
        }
    }
}
