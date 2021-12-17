using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Close : MonoBehaviour
{
    public GameObject portal;
    public string object_to_find;
    
    // Start is called before the first frame update
    void Start()
    {
        //portal = GameObject.Find("next_level");
        portal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find(object_to_find)){
            portal.SetActive(true);
        }
    }
}
