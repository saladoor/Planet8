using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {

	public Transform player; //Transform of player
	public GameObject b; //Bullet object

	private float shootingInterval = 2.5f;

	// Use this for initialization
	void Start () {
		InvokeRepeating("shootBullet", 0f, shootingInterval);
	}

	void shootBullet()
	{
		float spawnPosX = transform.position.x;
		float spawnPosY = transform.position.y;

		Vector2 spawnPos = new Vector2(spawnPosX, spawnPosY);
		Vector2 movement = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
		Object inst = Instantiate(b, spawnPos, Quaternion.identity);
		GameObject bullet = inst as GameObject;
		bullet.GetComponent<BulletController>().shoot(movement);
	}	
}
