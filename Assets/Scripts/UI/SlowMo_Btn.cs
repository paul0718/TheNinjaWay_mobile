using UnityEngine;
using UnityEngine.UI;

public class SlowMo_Btn : MonoBehaviour {
    [SerializeField]
	public Button SlowMoBtn;
	AudioSource _audioSource; // NEED TO ADD "AUDIO SOURCE" COMPONENT TO THE BUTTON
    public AudioClip slowSnd; 
	public float timer;
	public float SlowMo_Interval;


	void Start () {
		SlowMoBtn = GameObject.FindWithTag("SlowMoBtn").GetComponent<Button>();
		SlowMoBtn.onClick.AddListener(TaskOnClick);
		_audioSource = GetComponent<AudioSource>();
        slowSnd = (AudioClip)Resources.Load("Audio/slowSnd"); // add file "slowSnd" to Resources/Audio
        // PublicVars.has_SlowMo_item = true; // for testing
		PublicVars.has_SlowMo_item = false;
        PublicVars.slowMo_active = false;
		SlowMo_Interval = 5;
        timer = SlowMo_Interval;
	}


    // Update is called once per frame
    void Update()
    {
	    if(PublicVars.has_SlowMo_item)
        {
            SlowMoBtn.interactable = true;
        } 
        else{
            SlowMoBtn.interactable = false;
        }

        if (PublicVars.slowMo_active)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = SlowMo_Interval;
                PublicVars.slowMo_active = false;
                Time.timeScale = 1;
            }
        }
    }

	void TaskOnClick(){
		PublicVars.has_SlowMo_item = false;
		PublicVars.slowMo_active = true;
		Time.timeScale = 0.5f;
        _audioSource.PlayOneShot(slowSnd);
	}
}
