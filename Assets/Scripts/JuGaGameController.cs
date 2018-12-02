using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JuGaGameController : MonoBehaviour {

	public GameObject[] foods;
	public Vector3 spawnValues;
	public int foodCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnWaves ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true) 
		{
			for (int i = 0; i < foodCount; i++) 
			{
				GameObject food = foods[Random.Range (0,foods.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (food, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			}
		}
	}
