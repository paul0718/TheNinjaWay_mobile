using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCam : MonoBehaviour
{
    public float length, startpos;
    public GameObject player;
    public float parallaxEffect;

    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float temp = (player.transform.position.x * (1-parallaxEffect));
        float distance = (player.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length*2;
        else if (temp < startpos - length) startpos -= length*2;
    }
}
