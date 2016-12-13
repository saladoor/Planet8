using UnityEngine;
using System.Collections;

public class MineController : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		if (!(col.tag == "Player" || col.tag == "slowmo"))
		{
			Destroy(this.gameObject);
		}
	}
}
