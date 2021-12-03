using UnityEngine;
using UnityEngine.UI;

public class Jump_Btn : MonoBehaviour {
    [SerializeField]
	public Button JmpBtn;
    public Rigidbody2D player_rigid;

	void Start () {
        player_rigid = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
		JmpBtn = GameObject.FindWithTag("JmpBtn").GetComponent<Button>();
		JmpBtn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
        player_rigid.velocity = new Vector2(player_rigid.velocity.x, 0);
        player_rigid.AddForce(new Vector2(0, 500));
		Debug.Log ("You have clicked the button!");
	}
}
