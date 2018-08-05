﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class Oscillator : MonoBehaviour {

    [SerializeField] Vector3 movementVector;

	// TODO remove from inspector later
	// 0 for not moved, 1 for fully moved.
	[Range(0,1)][SerializeField]float movementFactor;

	Vector3 startingPos;

	// Use this for initialization
	void Start () {
		startingPos = transform.position;	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 offset = movementVector * movementFactor;
		transform.position = startingPos + offset;
	}
}
