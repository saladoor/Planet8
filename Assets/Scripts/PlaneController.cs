using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour
{

	public float rotationSpeed = 100f;
	public float movespeed = 1500f;
	private float maxSpeed = 3.0f;

	private Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void FixedUpdate()
	{
        float rot = -1 * rotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        if (!Input.GetKey(KeyCode.UpArrow))
        {
            rot = rot*1.8f;
        }

		transform.Rotate(0, 0, rot);

		float l = movespeed * Input.GetAxis("Vertical") * Time.deltaTime;
		rb.AddRelativeForce(new Vector2(0, l));

		//Make sure we can't move too fast
		if(rb.velocity.magnitude > maxSpeed)
		{
			float veloX = rb.velocity.normalized.x * maxSpeed;
			float veloY = rb.velocity.normalized.y * maxSpeed;
			rb.velocity = new Vector2(veloX, veloY);
		}
	}
}
