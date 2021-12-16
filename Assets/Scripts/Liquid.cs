using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{

    public bool isLava = false;
    public AudioClip hitSnd;
    AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        hitSnd = (AudioClip)Resources.Load("Audio/Hit");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player") {
            other.GetComponent<Player>().StartCoroutine("slowDown");
            if (isLava){
                other.GetComponent<Player>().getHit(1f);
                FindObjectOfType<HP>().loseHealth(0.5f);
                _audioSource.PlayOneShot(hitSnd);
            }
        }
    }
}
