using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    public float speed = 0f;
    // Update is called once per frame
    void Update()
    {
        // pausemenu
        if (PublicVars.paused) return;
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.y <= -10) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            speed = 2f;
        }
        if (other.gameObject.CompareTag("Liquid"))
        {
            Destroy(gameObject);
        }
    }

}
