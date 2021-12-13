using UnityEngine;
using UnityEngine.UI;

public class Warp_Btn : MonoBehaviour
{
    [SerializeField]
	Button WarpBtn;
    Rigidbody2D player_rigid;


    public Image warpActive;
    public Image warpInactive;
    public float distance = 10;
    public GameObject smokerPrefab;
    public Transform smokeSpawnPos;

    AudioSource _audioSource; // NEED TO ADD "AUDIO SOURCE" COMPONENT TO THE BUTTON
    public AudioClip warpSnd;
    float warpCooldown = 3;
    float nextWarp = 0;


    // Vector2 oldPos;
    // Vector2 newPos;
    public bool facingLeft = false;

	void Start () {
        player_rigid = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
		WarpBtn = GameObject.FindWithTag("WarpBtn").GetComponent<Button>();
		WarpBtn.onClick.AddListener(TaskOnClick);
        _audioSource = GetComponent<AudioSource>();
        warpSnd = (AudioClip)Resources.Load("Audio/Warp"); // add file "slowSnd" to Resources/Audio
	}

    void Update(){
        CheckMoveDirection();
        if(Time.time > nextWarp)
        {
            warpActive.gameObject.SetActive(true);
            warpInactive.gameObject.SetActive(false);
        } 
        else {
            warpActive.gameObject.SetActive(false);
            warpInactive.gameObject.SetActive(true);
        }
    }

	void TaskOnClick(){
        if(Time.time > nextWarp)
        {
            nextWarp = Time.time + warpCooldown;
            _audioSource.PlayOneShot(warpSnd);
            GameObject newSmoke = Instantiate(smokerPrefab, smokeSpawnPos.position, Quaternion.identity);
            if(facingLeft){
                player_rigid.transform.position = new Vector2(player_rigid.transform.position.x-distance,player_rigid.transform.position.y); 
            }
            else{
                player_rigid.transform.position = new Vector2(player_rigid.transform.position.x+distance,player_rigid.transform.position.y);      
            }
            GameObject newSmoke2 = Instantiate(smokerPrefab, smokeSpawnPos.position, Quaternion.identity);
        }


        // newPos = transform.position;
	}

    void CheckMoveDirection()
    {
        if (player_rigid.transform.localScale.x < 0) {
            facingLeft = true;
        }
        else if (player_rigid.transform.localScale.x > 0){
            facingLeft = false;
        }
    }

}



