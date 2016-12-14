using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform t; //Players transform

	void FixedUpdate()
	{
		transform.position = new Vector3(t.position.x, t.position.y, transform.position.z);

        //transform.position = Vector3.Lerp(transform.position, t.position, 3f) + new Vector3(0, 0, -10);


	}
}
