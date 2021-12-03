using UnityEngine;
using UnityEngine.UI;

public class Fire_Btn : MonoBehaviour
{
    [SerializeField]
	Button FireBtn;
    Rigidbody2D player_rigid;

    public GameObject shurikenPrefab;

	void Start () {
        player_rigid = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
		FireBtn = GameObject.FindWithTag("FireBtn").GetComponent<Button>();
		FireBtn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
        //nextShuriken = Time.time + shurikenCooldown;
        //_audioSource.PlayOneShot(shurikenSnd);
        GameObject newShuriken = Instantiate(shurikenPrefab, player_rigid.transform.position, Quaternion.identity);
        newShuriken.GetComponent<Rigidbody2D>().AddForce(new Vector2(2600 * player_rigid.transform.localScale.x, 0));
	}
}
