﻿using UnityEngine;
using System.Collections;

public class checkpointController : MonoBehaviour {
    public GameOverManager gm;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        gm.startingPoint = transform.position;
    }
}
