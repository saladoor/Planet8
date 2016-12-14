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
	public float slowMoMaxSpeedMultiplier = 0.65f;

	private Rigidbody2D rb;
	public bool slowmoStart = false;
	public bool slowmoEnd = false;
	public float slowdown;

	private bool recentlyTransitioned = false;

    PlayerHealth playerHealth;
	public GameObject b; //Bullet object

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rotationSpeed = initRotationSpeed;
		noMoveRotMultiplier = initNoMoveRotMultiplier;
		movespeed = initMovespeed;
		maxSpeed = initMaxSpeed;
        playerHealth = GetComponent<PlayerHealth>();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		//Used for transitions
		if (col.tag == "Transition" && recentlyTransitioned == false)
		{
			recentlyTransitioned = true;
			TransitionController transPair = col.GetComponent<TransitionController>().transPair;
			float secuMove = transPair.securityMove;
			float rotZ = transPair.transform.rotation.eulerAngles.z;

			//move the plane to pair position and then move it slightly outwards from along the pairs z-angle.
			Vector3 v = (new Vector3(Mathf.Cos(rotZ * Mathf.Deg2Rad), Mathf.Sin(rotZ * Mathf.Deg2Rad), 0))*secuMove;
			transform.position = transPair.transform.position + v;
		}
        else if  (col.tag == "Threat")
        {
            playerHealth.TakeDamage(15);
        }


    }

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Transition" && recentlyTransitioned == true)
		{
			recentlyTransitioned = false;
		}
	}

	void Update()
	{
		if (Input.GetKeyDown("space"))
		{
				Object inst = Instantiate(b, transform.position, Quaternion.identity);
				GameObject bullet = inst as GameObject;
				bullet.GetComponent<FriendlyBulletController>().shoot(transform.up);
		}
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
