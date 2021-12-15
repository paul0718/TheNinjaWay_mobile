using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    public int speed = 3;
    public float returnSpeed = 2.5f;
    public int damage = 1;
    public int enemyHP = 20;
    public float followRadius = 3.0f;
    public float attackRadius = 1.5f;

    GameObject player;
    Rigidbody2D player_rb;
    Collider2D _collider;
    float attackCooldown = 3;
    float nextAttack;

    Vector3 startPos;

    Rigidbody2D _rigidbody;
    AudioSource _audioSource;
    public AudioClip hitSnd;
    Vector2 character;
    float dist;

    bool isFollowing = false;
    bool shouldFollow = false;
    
    Animator _animator;

    SpriteRenderer noticed;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        hitSnd = (AudioClip)Resources.Load("Audio/Hit");
        FindObjectOfType<HP>().setDefaultHealthPoint(10);
        startPos = transform.position;
        player = GameObject.FindWithTag("Player");
        player_rb = player.GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        noticed = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        dist = Vector2.Distance(player.transform.position, transform.position);
        character = transform.localScale;
        if (checkShouldFollow(dist)) {
            if (checkShouldAttack(dist) && Time.time > nextAttack) {
                
                StartCoroutine("bite");
            }
            else {
                // Player is in front of the enemy.
                if (shouldFollow) StartCoroutine("follow");
                
                if (isFollowing) {
                    if (player.transform.position.x < transform.position.x) {
                        this.transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
                        character.x = Math.Abs(character.x);
                    }
                    if (player.transform.position.x > transform.position.x) {
                        this.transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
                        character.x = -Math.Abs(character.x);
                    }
                }
                
                
            }
        } else {
            _animator.SetBool("follow", false);
            _collider.enabled = true;
            isFollowing = false;
            shouldFollow = true;
        }
        // transform.localScale = character;
        if(enemyHP <= 0)
        {
            Destroy(gameObject);
        }

    }

    // Return true if the enemy should chase the player.
    bool checkShouldFollow(float dist)
    {
        if (dist < followRadius) {  return true; }
        return false;
    }

    // Return true if the enemy should attack the player.
    bool checkShouldAttack(float dist)
    {
        if (dist < attackRadius) { return true; }
        return false;
    }

    IEnumerator follow()
    {   
        shouldFollow = false;
        noticed.enabled = true;
        yield return new WaitForSeconds(1f);
        _collider.enabled = false;
        _animator.SetBool("follow", true);
        isFollowing = true;
        noticed.enabled = false;
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Shuriken")){
            enemyHP -= 10;
            _audioSource.PlayOneShot(hitSnd);
            Destroy(other.gameObject);
        }
        //else if(other.CompareTag("Cut")){
        //    _audioSource.PlayOneShot(hitSnd);
        //    Destroy(other.gameObject);
        //}
    }

    IEnumerator bite() {
        _animator.SetTrigger("attack");
       
        nextAttack = Time.time + attackCooldown;
         yield return new WaitForSeconds(0.5f);
        _audioSource.PlayOneShot(hitSnd);
        FindObjectOfType<HP>().loseHealth(damage);
        player.GetComponent<Player>().getHit((player.transform.position.x - transform.position.x)*100);
        //player_rb.AddForce(new Vector2(, 350));
        yield return null;
    }
}

