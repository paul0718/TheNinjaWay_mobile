using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Key_Move : MonoBehaviour
{
    public int speed = 6;
    public int jumpForce = 500;
    int jumps;
    float xSpeed = 0;
    Vector2 oldPos;
    Vector2 newPos;
    
    public LayerMask groundLayer;
    public Transform feet;


    public HP hp;

    public bool facingLeft = false;
    public bool grounded = false;

    Rigidbody2D _rigidbody;
    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        xSpeed = Input.GetAxis("Horizontal") * speed;
        _animator.SetFloat("Speed", Mathf.Abs(xSpeed));
        _animator.SetFloat("YSpeed", Mathf.Abs(_rigidbody.velocity.y));
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        if((facingLeft == false && transform.localScale.x < 0) || (facingLeft == true && transform.localScale.x > 0) )
        {
            transform.localScale *= new Vector2(-1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PublicVars.paused) return;
        CheckMoveDirection();
        grounded = Physics2D.OverlapCircle(feet.position, .3f, groundLayer);
        oldPos = newPos;
        if(grounded)
        {
            jumps = 1;
        }

        if(Input.GetKeyDown("space") && (jumps > 0 || grounded))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, jumpForce));
            jumps--;
        }
        newPos = transform.position;
        if(transform.position.y <= -30)
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
}
