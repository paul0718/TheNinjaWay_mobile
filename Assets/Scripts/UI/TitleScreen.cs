using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene("0");
    }

    public void LoadLevel(){
        SceneManager.LoadScene("LvlLoader");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
