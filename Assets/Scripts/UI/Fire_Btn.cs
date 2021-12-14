using UnityEngine;
using UnityEngine.UI;

public class Fire_Btn : MonoBehaviour
{
    [SerializeField]
	Button FireBtn;
    Rigidbody2D player_rigid;
	AudioSource _audioSource; // NEED TO ADD "AUDIO SOURCE" COMPONENT TO THE BUTTON

	//Shuriken Related
	public int shurikenForce = 2600;
    float shurikenCooldown = 0.7f;
    float nextShuriken = 0;
	public AudioClip shurikenSnd; 
    public GameObject shurikenPrefab;

	//Cut Related
	public int cutForce = 1300;
	public AudioClip cutSnd;
	public GameObject cutPrefab;
	public float timer;
	public int blade_interval = 60; //time period

	void Start () {
		FireBtn = GameObject.FindWithTag("FireBtn").GetComponent<Button>();
		FireBtn.onClick.AddListener(TaskOnClick);
		player_rigid = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
		_audioSource = GetComponent<AudioSource>();

		shurikenSnd = (AudioClip)Resources.Load("Audio/ShurikenSnd");
		shurikenPrefab = (GameObject)Resources.Load("Prefabs/Shuriken");

		cutSnd = (AudioClip)Resources.Load("Audio/cutSnd"); // add file "cutSnd" to Resources/Audio
		cutPrefab = (GameObject)Resources.Load("Prefabs/Cut"); // add prefab "Cut" to Resources/Audio
        
		timer = blade_interval;
	}

    void Update()
    {
        if (PublicVars.blade_active)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = blade_interval;
                PublicVars.blade_active = false;
            }
        }
    }

	void TaskOnClick(){
		if (PublicVars.blade_active){
				_audioSource.PlayOneShot(cutSnd);
				GameObject newCut = Instantiate(cutPrefab, player_rigid.transform.position, Quaternion.identity);
				newCut.GetComponent<Rigidbody2D>().AddForce(new Vector2(cutForce * player_rigid.transform.localScale.x, 0));
		}
	    else{
			if(Time.time > nextShuriken)
			{
				nextShuriken = Time.time + shurikenCooldown;
				_audioSource.PlayOneShot(shurikenSnd);
				GameObject newShuriken = Instantiate(shurikenPrefab, player_rigid.transform.position, Quaternion.identity);
				newShuriken.GetComponent<Rigidbody2D>().AddForce(new Vector2(shurikenForce * player_rigid.transform.localScale.x, 0));
			}
		}
	}
}
