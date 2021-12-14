using UnityEngine;
using UnityEngine.UI;

public class Pause_Btn : MonoBehaviour
{
    [SerializeField]
	Button PauseBtn;

    public GameObject pauseMenu;

	void Start () {
		PauseBtn = GameObject.FindWithTag("PauseBtn").GetComponent<Button>();
		PauseBtn.onClick.AddListener(TaskOnClick);
        pauseMenu = GameObject.Find("Pause");
        pauseMenu.SetActive(false);
	}

	void TaskOnClick(){
        if(PublicVars.paused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            PublicVars.paused = false;
        } else 
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            PublicVars.paused = true;
        }
	}

}



