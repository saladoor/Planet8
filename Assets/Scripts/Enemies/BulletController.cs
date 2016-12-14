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
            //StartCoroutine(freezeFrame(0.02f));

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

    IEnumerator freezeFrame(float sec)
    {
        float temp = Time.timeScale;
        Time.timeScale = 0.000001f;
        yield return new WaitForSecondsRealtime(sec);
        Time.timeScale = temp;
        yield return null;
    }
}
