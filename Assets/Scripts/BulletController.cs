using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public Rigidbody2D rb;
	private float bulletSpeed = 2.0F;
	//private float time;

	void Start()
	{
		//time = Time.deltaTime;
	}

	void Update()
	{
		//time = Time.deltaTime;
	}

	public void shoot(Vector2 move)
	{
		rb.velocity = move.normalized * bulletSpeed;
	}
}
