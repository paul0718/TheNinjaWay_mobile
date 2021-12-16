using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 0.5f;
    public float distance = 5f;
    public bool horiz = false;
    public bool vert = false;
    public bool moveRight = false;
    float startX;
    public bool moveUp = false;
    float startY;
    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // pausemenu
        if (PublicVars.paused) return;
        //movement
        Vector2 newPosition = transform.position;
        if((horiz == true) && (vert == true)){
            if(moveRight){
                newPosition.x = Mathf.SmoothStep(startX, startX+distance, Mathf.PingPong(Time.time * speed,1));
            }else{
                newPosition.x = Mathf.SmoothStep(startX, startX-distance, Mathf.PingPong(Time.time * speed,1));
            }
            if(moveUp){
                newPosition.y = Mathf.SmoothStep(startY, startY+distance, Mathf.PingPong(Time.time * speed,1));
            }else{
                newPosition.y = Mathf.SmoothStep(startY, startY-distance, Mathf.PingPong(Time.time * speed,1));
            }
        }else if(horiz){  
            if(moveRight){
                newPosition.x = Mathf.SmoothStep(startX, startX+distance, Mathf.PingPong(Time.time * speed,1));
            }else{
                newPosition.x = Mathf.SmoothStep(startX, startX-distance, Mathf.PingPong(Time.time * speed,1));
            }
        }else if(vert){
            if(moveUp){
                newPosition.y = Mathf.SmoothStep(startY, startY+distance, Mathf.PingPong(Time.time * speed,1));
            }else{
                newPosition.y = Mathf.SmoothStep(startY, startY-distance, Mathf.PingPong(Time.time * speed,1));
            }
        }
        transform.position = newPosition;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            other.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            other.transform.SetParent(null);
        }
    }
}
