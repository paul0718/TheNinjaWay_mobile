using UnityEngine;
using UnityEngine.UI;

public class Warp_Btn : MonoBehaviour
{
    [SerializeField]
	Button WarpBtn;
    GameObject player;
    //Rigidbody2D player_rigid;


    public GameObject warpActive;
    public GameObject warpInactive;
    public float distance = 10;
    public GameObject smokerPrefab;
    public Transform smokeSpawnPos;

    AudioSource _audioSource; // NEED TO ADD "AUDIO SOURCE" COMPONENT TO THE BUTTON
    public AudioClip warpSnd;
    float warpCooldown = 3;
    float nextWarp = 0;


    // Vector2 oldPos;
    // Vector2 newPos;
    //public bool PublicVars.facingLeft = false;

	void Start () {
        player = GameObject.FindWithTag("Player");
        //player_rigid = player.GetComponent<Rigidbody2D>();
		WarpBtn = GameObject.FindWithTag("WarpBtn").GetComponent<Button>();
		WarpBtn.onClick.AddListener(TaskOnClick);
        _audioSource = GetComponent<AudioSource>();
        warpSnd = (AudioClip)Resources.Load("Audio/Warp"); // add file "slowSnd" to Resources/Audio
        smokerPrefab = (GameObject)Resources.Load("Prefabs/Smoke");
        smokeSpawnPos = GameObject.FindWithTag("Feet").transform;

        warpActive = GameObject.Find("WarpActive");
        warpInactive = GameObject.Find("WarpInactive");
	}

    void Update(){
        // CheckMoveDirection();
        if(Time.time > nextWarp)
        {
            // warpActive.gameObject.SetActive(true);
            // warpInactive.gameObject.SetActive(false);
            warpActive.SetActive(true);
            warpInactive.SetActive(false);
        } 
        else {
            // warpActive.gameObject.SetActive(false);
            // warpInactive.gameObject.SetActive(true);
            warpActive.SetActive(false);
            warpInactive.SetActive(true);
        }
    }

	void TaskOnClick(){
        if(Time.time > nextWarp)
        {
            nextWarp = Time.time + warpCooldown;
            _audioSource.PlayOneShot(warpSnd);
            GameObject newSmoke = Instantiate(smokerPrefab, smokeSpawnPos.position, Quaternion.identity);
            if(PublicVars.facingLeft){
                player.transform.position = new Vector2(player.transform.position.x-distance,player.transform.position.y); 
            }
            else{
                player.transform.position = new Vector2(player.transform.position.x+distance,player.transform.position.y);      
            }
            GameObject newSmoke2 = Instantiate(smokerPrefab, smokeSpawnPos.position, Quaternion.identity);
        }


        // newPos = transform.position;
	}

    // void CheckMoveDirection()
    // {
    //     if (player.transform.localScale.x < 0) {
    //         PublicVars.facingLeft = true;
    //     }
    //     else if (player.transform.localScale.x > 0){
    //         PublicVars.facingLeft = false;
    //     }
    // }

}



