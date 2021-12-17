using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miniboss : MonoBehaviour
{
    public int speed = 3;
    public int enemyHP = 100;
    public float followRadius = 10.0f;
    public float attackRadius = 10.0f;

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

    bool isFollowing = false;
    bool shouldFollow = false;

    public GameObject shurikenPrefab;
    public AudioClip shurikenSnd; 
    public int shurikenForce = 2600;
    public GameObject smokerPrefab;
    public AudioClip warpSnd;
    public int distance = 10;
    float warpCooldown = 5;
    float nextWarp;
    
    Animator _animator;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        hitSnd = (AudioClip)Resources.Load("Audio/Hit");
        startPos = transform.position;
        player = GameObject.FindWithTag("Player");
        player_rb = player.GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
		shurikenSnd = (AudioClip)Resources.Load("Audio/ShurikenSnd");
		shurikenPrefab = (GameObject)Resources.Load("Prefabs/EnemyShuriken");
        warpSnd = (AudioClip)Resources.Load("Audio/Warp");
        smokerPrefab = (GameObject)Resources.Load("Prefabs/Smoke");
        
    }

    void FixedUpdate()
    {
        dist = Vector2.Distance(player.transform.position, transform.position);
        enemy_localscale = transform.localScale;
        if (checkShouldFollow(dist)) {
            if (Time.time > nextWarp) {
                tp_func();
            }
            if (checkShouldAttack(dist) && Time.time > nextAttack) {
                StartCoroutine("attack");
            }
            else {
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
            _animator.SetBool("Following", false);
            isFollowing = false;
            shouldFollow = true;
        }
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
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("Following", true);
        isFollowing = true;
        yield return null;
    }


    IEnumerator attack() {
        _audioSource.PlayOneShot(shurikenSnd);
        GameObject newShuriken = Instantiate(shurikenPrefab, transform.position, Quaternion.identity);
        if (player.transform.position.x < transform.position.x) {
            newShuriken.GetComponent<Rigidbody2D>().AddForce(new Vector2(-shurikenForce, 0));
        }
        else{
            newShuriken.GetComponent<Rigidbody2D>().AddForce(new Vector2(shurikenForce, 0));
        }
       
        nextAttack = Time.time + attackCooldown;
        yield return new WaitForSeconds(0.5f);
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

    private void tp_func(){
        _audioSource.PlayOneShot(warpSnd);
        GameObject newSmoke = Instantiate(smokerPrefab, transform.position, Quaternion.identity);
        if(player.transform.position.x < transform.position.x){
            transform.position = new Vector2(transform.position.x-distance,transform.position.y); 
        }
        else{
            transform.position = new Vector2(transform.position.x+distance,transform.position.y);      
        }
        GameObject newSmoke2 = Instantiate(smokerPrefab, transform.position, Quaternion.identity);
        nextWarp = Time.time + warpCooldown;
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

