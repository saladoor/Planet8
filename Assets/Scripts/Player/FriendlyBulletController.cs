using UnityEngine;
using System.Collections;

public class FriendlyBulletController : MonoBehaviour
{

	public Rigidbody2D rb;
	public CircleCollider2D cc;
	public float bulletSpeed = 4.0F;

	public void shoot(Vector2 move)
	{
		GetComponent<AudioSource>().Play();
		rb.velocity = move.normalized * bulletSpeed;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!(col.tag == "Player" || col.tag == "slowmo" || col.tag == "FriendlyBullet"))
        {
			rb.velocity = new Vector3(0f, 0f, 0f);
			IEnumerator c = coDestroy(this.gameObject);
            StartCoroutine(c);
        }
    }

	void Update()
	{
		GetComponent<AudioSource>().pitch = Time.timeScale;
	}

	IEnumerator coDestroy(GameObject g)
    {
		GetComponent<Animator>().SetBool("Dead", true); //Sets off the death animation
		yield return new WaitForSeconds(0.5f); //8 (8/60) frames of animation later we move the plane

		cc.transform.position = new Vector3(0f, 17f, 0f);

		yield return new WaitForSecondsRealtime(1);
        Destroy(g);
        yield return null;
    }

}
