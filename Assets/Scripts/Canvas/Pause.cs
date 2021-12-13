using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{

    public GameObject pauseUI;

    public void ContinueGame()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        PublicVars.paused = false;
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        PublicVars.paused = false;
        SceneManager.LoadScene("TitleScreen");
    }
}
