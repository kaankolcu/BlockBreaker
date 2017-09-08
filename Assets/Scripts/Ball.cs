using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Paddle paddle;

	private Vector3 paddleToBallVector;
	private bool hasStarted = false;

	// Use this for initialization
	void Start () {
		paddle = GameObject.FindObjectOfType<Paddle>();
		paddleToBallVector = this.transform.position - paddle.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasStarted) {
			//at the start ensure ball stays on paddle until left mouse btn clicked.
			this.transform.position = paddle.transform.position + paddleToBallVector;
			if (Input.GetMouseButtonDown(0)) {
				//wait for a mouse press to launch
				hasStarted = true;
				this.rigidbody2D.velocity = new Vector2(2f,10f);
				audio.Play();
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		Vector2 tweak = new Vector2 (Random.Range (0f,0.2f),Random.Range (0f,0.2f));

		rigidbody2D.velocity += tweak;
	}
}
