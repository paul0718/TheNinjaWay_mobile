using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textBox : MonoBehaviour
{
    public string dialog;
    private bool showGUI = false;
    public int x1;
    public int y1;
    public int x2;
    public int y2;
    //haha sorry - R
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            showGUI = true;
        }    
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            showGUI = false;
        }
    }

    void OnGUI() {
        if(showGUI){
            GUI.Box(new Rect(x1,y1,x2,y2), dialog);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
