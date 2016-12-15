using UnityEngine;
using System.Collections;

public class MineController : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!(col.tag == "Player" || col.tag == "slowmo"))
        {
			GetComponent<AudioSource>().Play();
            IEnumerator c = coDestroy(this.gameObject);
            StartCoroutine(c);
        }
        if (col.tag == "FriendlyBullet")
        {
			PlayerScore.score += PlayerScore.mineScore;
        }
    }

    IEnumerator coDestroy(GameObject g)
    {
		GetComponent<Animator>().SetBool("Dead", true); //Sets off the death animation
		yield return new WaitForSeconds(0.5f); //8 (8/60) frames of animation later we move the plane

		transform.position = new Vector3(0f, 19f, 0f);

		yield return new WaitForSecondsRealtime(1);
        Destroy(g);
        yield return null;
    }

	void Update()
	{
		GetComponent<AudioSource>().pitch = Time.timeScale;
	}
}
