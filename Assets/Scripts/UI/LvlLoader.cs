using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public void Load_0()
    {
        SceneManager.LoadScene("0");
    }

    public void Load_1_1()
    {
        SceneManager.LoadScene("1-1");
    }

    public void Load_1_2()
    {
        SceneManager.LoadScene("1-2");
    }

    public void Load_2_1()
    {
        SceneManager.LoadScene("2-1");
    }

    public void Load_2_2()
    {
        SceneManager.LoadScene("2-2");
    }

    public void Load_3()
    {
        SceneManager.LoadScene("3");
    }

    public void Load_4_1()
    {
        SceneManager.LoadScene("4-1");
    }

    public void Load_4_2()
    {
        SceneManager.LoadScene("4-2");
    }

    public void Load_5_1()
    {
        SceneManager.LoadScene("5-1");
    }

    public void Load_5_2()
    {
        SceneManager.LoadScene("5-2");
    }

    public void Load_6()
    {
        SceneManager.LoadScene("6");
    }

    public void Back(){
        SceneManager.LoadScene("TitleScreen");
    }
}
