using UnityEngine;
using System.Collections;

public class MineController : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!(col.tag == "Player" || col.tag == "slowmo"))
        {
            transform.position = new Vector3(0f, 19f, 0f);
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
        yield return new WaitForSecondsRealtime(1);
        Destroy(g);
        yield return null;
    }


}
