using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public Rigidbody2D rb;
	public CircleCollider2D cc;
	public float bulletSpeed = 2.0F;

	public void shoot(Vector2 move)
	{
		rb.velocity = move.normalized * bulletSpeed;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(!(col.tag == "Player" || col.tag == "slowmo"))
		{
			cc.transform.position = new Vector3(0f, 15f, 0f);
			rb.velocity = new Vector3(0f, 0f, 0f);
			//Destroy(this.gameObject);
		}
	}
}
