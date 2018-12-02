using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuGaDestroyFood : MonoBehaviour {

	private JuGaGameController gameController;
	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<JuGaGameController>();
		}
		if (gameControllerObject == null) {
			Debug.Log ("Cannot find GameController Script");
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
		{
		if (other.CompareTag ("Ground") || other.CompareTag ("Wall"))
		{
			Destroy(gameObject);
		}

		}
}
