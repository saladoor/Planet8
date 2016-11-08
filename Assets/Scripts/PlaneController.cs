using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour
{

	private float rotationSpeed;
	private float noMoveRotMultiplier;
	private float movespeed;
	private float maxSpeed;

	public float initRotationSpeed = 130f;
	public float initNoMoveRotMultiplier = 1.7f;
	public float initMovespeed = 1500f;
	public float initMaxSpeed = 3.0f;

	public float slowmoRotationMultiplier = 1.0f;
	public float slowmoNoMoveRotMultiplier = 0.65f;
	public float slowmoMovespeedMultiplier = 10.0f;
	public float slowMoMaxSpeedMultiplier = 0.66f;

	private Rigidbody2D rb;
	public bool slowmoStart = false;
	public bool slowmoEnd = false;
	public float slowdown;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rotationSpeed = initRotationSpeed;
		noMoveRotMultiplier = initNoMoveRotMultiplier;
		movespeed = initMovespeed;
		maxSpeed = initMaxSpeed;
	}

	void FixedUpdate()
	{
		//When slowmotion starts we change the speed of the plane
		if (slowmoStart)
		{
			rotationSpeed = initRotationSpeed * (1 / slowdown) * slowmoRotationMultiplier;
			movespeed = initMovespeed * (1 / slowdown) * slowmoMovespeedMultiplier;
			maxSpeed = initMaxSpeed * slowMoMaxSpeedMultiplier;
			noMoveRotMultiplier = slowmoNoMoveRotMultiplier; //Changing a multiplier

			slowmoStart = false;
		}
		
		//When slowmotion ends we change it back
		if (slowmoEnd)
		{
			rotationSpeed = initRotationSpeed;
			movespeed = initMovespeed;
			maxSpeed = initMaxSpeed;
			noMoveRotMultiplier = initNoMoveRotMultiplier; //Changing the multiplier back
			slowmoEnd = false;
		}

		//Rotating movement
		float rot = -1 * rotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        if (!Input.GetKey(KeyCode.UpArrow))
        {
            rot = rot * noMoveRotMultiplier;
        }

		transform.Rotate(0, 0, rot);

		//Straight line movement
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
