using UnityEngine;
using System.Collections;

public class DodgeThisController : MonoBehaviour {

    public float rotationSpeed = 200.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
	}
}
