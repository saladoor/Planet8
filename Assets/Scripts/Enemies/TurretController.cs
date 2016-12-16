﻿using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {

	public Transform player; //Transform of player
	public GameObject b; //Bullet object
	private int disableRange = 15;

	private float shootingInterval = 2.5f;

	// Use this for initialization
	void Start () {
		InvokeRepeating("shootBullet", 0f, shootingInterval);
	}

	void shootBullet()
	{
		if(Mathf.Abs(player.position.x - transform.position.x) < disableRange && Mathf.Abs(player.position.y - transform.position.y) < disableRange)
		{
			Vector2 spawnPos = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y - 0.64f), player.position, 1.0f); //Hardcoded radius!

			Vector2 movement = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
			GetComponent<AudioSource>().Play(); //Sound!
			Object inst = Instantiate(b, spawnPos, Quaternion.identity);
			GameObject bullet = inst as GameObject;
			bullet.GetComponent<BulletController>().shoot(movement);
		}
	}	
}
