using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour
{
    public Slider slider;
    public GameObject gameOverMenu;

    public void setDefaultHealthPoint(float healthpoint)
    {
        slider.maxValue = healthpoint;
        slider.value = healthpoint;
    }

    public float getHealthPoint()
    {
        return slider.value;
    }

    public void setHealthPoint(float healthpoint)
    {
        slider.value = healthpoint;
    }

    public void loseHealth(float damage)
    {
        slider.value -= damage;
    }

    private void Start(){
        Time.timeScale = 1;
        gameOverMenu = GameObject.Find("Game Over");
        gameOverMenu.SetActive(false);
    }
    private void Update(){
        //if(FindObjectOfType<HP>().getHealthPoint() <= 0)
        if(getHealthPoint() <= 0)
        {
            Time.timeScale = 0;
            gameOverMenu.SetActive(true);
        }
    }

    //Fixed deltatime, TimeScale
}
