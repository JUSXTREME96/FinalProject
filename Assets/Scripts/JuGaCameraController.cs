﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuGaCameraController : MonoBehaviour {

	//public GameObject player;
	public GameObject player;

	public Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}

	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
		//transform.position = player.transform.position + offset; 
	}
}
