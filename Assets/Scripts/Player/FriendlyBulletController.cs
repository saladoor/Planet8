using UnityEngine;
using System.Collections;

public class FriendlyBulletController : MonoBehaviour
{

	public Rigidbody2D rb;
	public CircleCollider2D cc;
	public float bulletSpeed = 4.0F;

	public void shoot(Vector2 move)
	{
		rb.velocity = move.normalized * bulletSpeed;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!(col.tag == "Player" || col.tag == "slowmo"))
        {
            cc.transform.position = new Vector3(0f, 15f, 0f);
            rb.velocity = new Vector3(0f, 0f, 0f);
            IEnumerator c = coDestroy(this.gameObject);
            StartCoroutine(c);
        }
    }

    IEnumerator coDestroy(GameObject g)
    {
        yield return new WaitForSecondsRealtime(1);
        Destroy(g);
        yield return null;
    }

}
