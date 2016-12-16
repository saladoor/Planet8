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
		if(!(/*col.tag == "Player" || */col.tag == "slowmo"))
		{
			rb.velocity = new Vector3(0f, 0f, 0f);
			GetComponent<AudioSource>().Play();
			IEnumerator c = coDestroy(this.gameObject);
            StartCoroutine(c);
		}
        if (col.tag == "FriendlyBullet")
        {
            PlayerScore.score += PlayerScore.bulletScore    ;   
        }

    }

	void Update()
	{
		GetComponent<AudioSource>().pitch = Time.timeScale;
	}

	IEnumerator coDestroy(GameObject g)
    {
		GetComponent<Animator>().SetBool("Dead", true); //Sets off the death animation
		yield return new WaitForSeconds(0.3f); //8 (8/60) frames of animation later we move the plane

		cc.transform.position = new Vector3(0f, 15f, 0f);

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
