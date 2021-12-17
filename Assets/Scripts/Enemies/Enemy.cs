using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int speed = 3;
    public float returnSpeed = 2.5f;
    public int damage = 1;
    public int enemyHP = 20;
    public float followRadius = 3.0f;
    public float attackRadius = 1.5f;

    GameObject player;
    Rigidbody2D player_rb;
    float attackCooldown = 3;
    float nextAttack;

    Vector3 startPos;

    Rigidbody2D _rigidbody;
    AudioSource _audioSource;
    public AudioClip hitSnd;
    Vector2 enemy_localscale;
    float dist;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        hitSnd = (AudioClip)Resources.Load("Audio/Hit");
        FindObjectOfType<HP>().setDefaultHealthPoint(10);
        startPos = transform.position;
        player = GameObject.FindWithTag("Player");
        player_rb = player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        dist = Vector2.Distance(player.transform.position, transform.position);
        enemy_localscale = transform.localScale;
        if (checkShouldFollow(dist)) {
            if (checkShouldAttack(dist) && Time.time > nextAttack) {
                attack();
            }
            else {
                follow();
            }
        }
        // transform.localScale = enemy_localscale;
        if(enemyHP <= 0)
        {
            die();
        }

    }



    // Return true if the enemy should chase the player.
    private bool checkShouldFollow(float dist)
    {
        if (dist < followRadius) {  return true; }
        return false;
    }

    // Return true if the enemy should attack the player.
    private bool checkShouldAttack(float dist)
    {
        if (dist < attackRadius) { return true; }
        return false;
    }

    private void follow(){
        // Player is in front of the enemy.
        if (player.transform.position.x < transform.position.x) {
            this.transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
            enemy_localscale.x = Math.Abs(enemy_localscale.x);
        }
        // Player is behind the enemy.
        if (player.transform.position.x > transform.position.x) {
            this.transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
            enemy_localscale.x = -Math.Abs(enemy_localscale.x);
        }
    }

    private void attack()
    {
        
        nextAttack = Time.time + attackCooldown;
        _audioSource.PlayOneShot(hitSnd);
        FindObjectOfType<HP>().loseHealth(damage);
        StartCoroutine(player.GetComponent<Player>().getHit((player.transform.position.x - transform.position.x)*100));   
        //player.GetComponent<Player>().getHit((player.transform.position.x - transform.position.x)*1000);
        
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
            //enemyHP -= 10;
            receiveDamage(10);
            Destroy(other.gameObject);
        }
        // else if(other.CompareTag("Player")){
        //     StartCoroutine(player.GetComponent<Player>().getHit((player.transform.position.x - transform.position.x)*100));
        // }
        // else if(other.CompareTag("Cut")){
        //     Destroy(other.gameObject);
        //     die();
        // }
    }
}