using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int speed = 1;
    public int enemyHP = 100;
    public float followRadius = 10.0f;
    public float attackRadius = 7.0f;

    GameObject player;
    Rigidbody2D player_rb;
    float attackCooldown = 3;
    float nextAttack;

    // Vector3 startPos;

    Rigidbody2D _rigidbody;
    AudioSource _audioSource;
    public AudioClip shootSnd;
    Vector2 character;
    float dist;

    public GameObject hookPrefab;
    public int hookForce = 800;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        // startPos = transform.position;
        player = GameObject.FindWithTag("Player");
        player_rb = player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        dist = Vector2.Distance(player.transform.position, transform.position);
        character = transform.localScale;
        if (checkShouldFollow(dist)) {
            if (checkShouldAttack(dist) && Time.time > nextAttack) {
                attack();
            }
            else {
                // Player is in front of the enemy.
                if (player.transform.position.x < transform.position.x) {
                    this.transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
                    if (transform.localScale.x < 0){
                            transform.localScale *= new Vector2(-1, 1);
                        }
                }
                // Player is behind the enemy.
                if (player.transform.position.x > transform.position.x) {
                    this.transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
                    if (transform.localScale.x >= 0){
                            transform.localScale *= new Vector2(-1, 1);
                        }
                }
            }
        }
        transform.localScale = character;
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

    private void attack()
    {
        _audioSource.PlayOneShot(shootSnd);//?
        GameObject newHook = Instantiate(hookPrefab, transform.position, Quaternion.identity);
        newHook.GetComponent<Rigidbody2D>().AddForce(new Vector2(hookForce * (-transform.localScale.x), 0));

        nextAttack = Time.time + attackCooldown;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Shuriken")){
            enemyHP -= 10;
            // _audioSource.PlayOneShot(hitSnd);//?
            Destroy(other.gameObject);
        }
    }
}
