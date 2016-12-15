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

    public float fireRate = 0.2f;
    private float nextActionTime = 0.0f;

	private bool recentlyTransitioned = false;

    PlayerHealth playerHealth;
	public GameObject b; //Bullet object

    private bool unkillable = false;

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
            GetComponent<AudioSource>().Play();
            playerHealth.TakeDamage(15);
            if(playerHealth.currentHealth < 0)
            {
                IEnumerator c = coDestroy(this.gameObject);
                StartCoroutine(c);
            }
        }
    }

    IEnumerator coDestroy(GameObject g)
    {
        GetComponent<SpriteRenderer>().enabled = false;

        GetComponentInChildren<Animator>().SetBool("dead", true); //Sets off the death animation
        //yield return new WaitForSeconds(0.5f); //8 (8/60) frames of animation later we move the plane

        //transform.position = new Vector3(0f, 21f, 0f); //Move gameobject
        //rb.velocity = new Vector3(0f, 0f, 0f);

        //yield return new WaitForSecondsRealtime(1);
        //Destroy(g);
        yield return null;
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
        if (Input.GetKey("space"))
        {
            if (Time.time > nextActionTime)
            {
                nextActionTime = Time.time + fireRate;
                Object inst = Instantiate(b, transform.position, Quaternion.identity);
                GameObject bullet = inst as GameObject;
                bullet.GetComponent<FriendlyBulletController>().shoot(transform.up);
            }
        }

        if (Input.GetKeyDown("u"))
        {
            if (!unkillable)
            {
                playerHealth.currentHealth = System.Int32.MaxValue;
                unkillable = true;
            } else
            {
                playerHealth.currentHealth = 100;
                unkillable = false;
            }
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
