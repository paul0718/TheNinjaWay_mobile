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
    Vector2 enemy_localscale;
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
        enemy_localscale = transform.localScale;
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
                        if (enemy_localscale.x < 0){
                            transform.localScale *= new Vector2(-1, 1);
                        }
                    }
                    if (player.transform.position.x > transform.position.x) {
                        this.transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
                        if (enemy_localscale.x >= 0){
                            transform.localScale *= new Vector2(-1, 1);
                        }
                    }
                } 
            }
        } else {
            _animator.SetBool("follow", false);
            _collider.enabled = true;
            isFollowing = false;
            shouldFollow = true;
        }
        // transform.localScale = enemy_localscale;
        if(enemyHP <= 0)
        {
            die();
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
        _collider.enabled = false; // this is now preventing the melee script to work, we need a collider for that, maybe the collider to Trigger?
        _animator.SetBool("follow", true);
        isFollowing = true;
        noticed.enabled = false;
        yield return null;
    }

    IEnumerator bite() {
        _animator.SetTrigger("attack");
       
        nextAttack = Time.time + attackCooldown;
        yield return new WaitForSeconds(0.5f);
        _audioSource.PlayOneShot(hitSnd);
        //player.GetComponent<Player>().getHit((player.transform.position.x - transform.position.x)*100);
        StartCoroutine(player.GetComponent<Player>().getHit((player.transform.position.x - transform.position.x)*10));
        FindObjectOfType<HP>().loseHealth(damage);
        //player_rb.AddForce(new Vector2(, 350));
        yield return null;
    }

    private void die(){
        //play die animation
        //play die audio
        Destroy(gameObject);
    }

    public void receiveDamage(int damage){
        enemyHP -= damage;
        _audioSource.PlayOneShot(hitSnd);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Shuriken")){
            receiveDamage(10);
            Destroy(other.gameObject);
        }
        // else if(other.CompareTag("Cut")){
        //    _audioSource.PlayOneShot(hitSnd);
        //    Destroy(other.gameObject);
        //    Destroy(gameObject);
        // }
    }

    
}

