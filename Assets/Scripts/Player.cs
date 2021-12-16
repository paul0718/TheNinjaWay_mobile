using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int speed = 4;
    //public int jumpForce = 500;
    //int jumps;
    float xSpeed = 0;
    Vector2 oldPos;
    Vector2 newPos;
    
    public LayerMask groundLayer;
    public Transform feet;

    public HP hp;

    public bool facingLeft = false;

    Rigidbody2D _rigidbody;
    Animator _animator;

    public Joystick virtual_js; //automatic finding joystick object?


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        //virtual_js = (Joystick)Resources.Load("Prefabs/Joystick");
    }

    void FixedUpdate()
    {
        if (virtual_js.Horizontal > 0.2f){
            xSpeed = speed;
        }
        else if (virtual_js.Horizontal < -0.2f){
            xSpeed = -speed;
        }
        else{
            xSpeed = 0;
        }
        _animator.SetFloat("Speed", Mathf.Abs(xSpeed));
        _animator.SetFloat("YSpeed", Mathf.Abs(_rigidbody.velocity.y));
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        if((facingLeft == false && transform.localScale.x < 0) || (facingLeft == true && transform.localScale.x > 0) )
        {
            transform.localScale *= new Vector2(-1, 1);
        }
		
    }

    void Update()
    {
        if(PublicVars.paused) return;
        CheckMoveDirection();
        PublicVars.player_grounded = Physics2D.OverlapCircle(feet.position, .3f, groundLayer);
        oldPos = newPos;
        if(PublicVars.player_grounded)
        {
            PublicVars.jumps = 2;
        }
        newPos = transform.position;
        if(transform.position.y <= -20)
        {
            hp.setHealthPoint(0);
        }

    }

    void CheckMoveDirection()
    {
        if (oldPos.x > newPos.x){
            facingLeft = true;
        } else if (oldPos.x < newPos.x){
            facingLeft = false;
        }
    }

    public void getHit(float xForce){
        _rigidbody.AddForce(new Vector2(xForce, 350));
    }

    IEnumerator slowDown() {
        speed = 3;
        yield return new WaitForSeconds(3f);
        speed = 6;
    }
}
