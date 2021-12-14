using UnityEngine;
using UnityEngine.UI;

public class Jump_Btn : MonoBehaviour {
    [SerializeField]
	public Button JmpBtn;
    public Rigidbody2D player_rigid;
	public int jumpForce = 500;

	AudioSource _audioSource;
    public AudioClip jumpSnd; // NEED TO ADD "AUDIO SOURCE" COMPONENT TO THE BUTTON

	void Start () {
        player_rigid = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
		JmpBtn = GameObject.FindWithTag("JmpBtn").GetComponent<Button>();
		JmpBtn.onClick.AddListener(TaskOnClick);
		_audioSource = GetComponent<AudioSource>();
		jumpSnd = (AudioClip)Resources.Load("Audio/Jump 2");
	}

	void TaskOnClick(){
		if (PublicVars.jumps > 0 || PublicVars.player_grounded){ //maybe because: jumpbutton clicked -> jump--(still consider grounded since very close to ground) -> Player.Update set jump to 2 again
			if (_audioSource == null) Debug.LogError("playerAudio is null on " + gameObject.name);
			if (jumpSnd == null) Debug.LogError("crashSound is null on " + gameObject.name);
			_audioSource.PlayOneShot(jumpSnd);
			player_rigid.velocity = new Vector2(player_rigid.velocity.x, 0);
			player_rigid.AddForce(new Vector2(0, jumpForce));
			//Debug.Log ("You have clicked the button!");
			PublicVars.jumps--;
			
		}

	}
}
