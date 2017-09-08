using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {


	public AudioClip brickSound;

	public Sprite[] hitSprites;

	public static int breakableCount = 0;

	private int timesHit;
	private LevelManager levelManager;

	private bool isBreakable;

	public GameObject smoke;



	// Use this for initialization
	void Start () {

		isBreakable = (this.tag == "Breakable");

		if(isBreakable){
			breakableCount++;
		}

		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		timesHit = 0;



	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (isBreakable) {
			AudioSource.PlayClipAtPoint(brickSound,transform.position);
			HandleHits ();
		}
	}

	void HandleHits(){
		timesHit++;
		int maxHits = hitSprites.Length + 1;
		if (timesHit >= maxHits) {
			breakableCount--;

			levelManager.BrickDestroyed();

			GameObject smokePuff = Instantiate(smoke, transform.position,Quaternion.identity) as GameObject;
			smokePuff.particleSystem.startColor = gameObject.GetComponent<SpriteRenderer>().color;

			Destroy (gameObject);
		} else {
			LoadSprites();
		}
	}

	void LoadSprites(){
		int spriteIndex = timesHit - 1;
		if (hitSprites [spriteIndex]) {
			this.GetComponent<SpriteRenderer> ().sprite = hitSprites [spriteIndex];
		} else {
			Debug.Log("Brick sprite missing");
		}
	}

	// TODO Remove this method once we can actually win!
	void SimulateWin(){
		levelManager.LoadNextLevel ();
	}
}
