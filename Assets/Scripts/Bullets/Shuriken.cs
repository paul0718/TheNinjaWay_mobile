using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    //public bool PublicVars.facingLeft = false;
    Vector2 oldPos;
    Vector2 newPos;
    public float speedRotate = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //CheckMoveDirection();
        transform.Rotate(Vector3.forward * speedRotate);
    }

    void FixedUpdate()
    {
        if((PublicVars.facingLeft == false && transform.localScale.x > 0) || (PublicVars.facingLeft == true && transform.localScale.x < 0) )
        {
            transform.localScale *= new Vector2(-1, 1);
        }
    }

    // void CheckMoveDirection()
    // {
    //     if (oldPos.x > newPos.x){
    //         PublicVars.facingLeft = true;
    //     } else if (oldPos.x < newPos.x){
    //         PublicVars.facingLeft = false;
    //     }
    // }
}
