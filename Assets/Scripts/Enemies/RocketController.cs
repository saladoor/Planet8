using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {

	public Transform playerTrans;
	public float rotationSpeed;
	public float movespeed;
	public float maxSpeed;
	public float maxAngle;

	private float playerToTheRight = 0;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (!(col.tag == "Player" || col.tag == "slowmo"))
		{
			transform.position = new Vector3(0f, 15f, 0f);
			rb.velocity = new Vector3(0f, 0f, 0f);
            IEnumerator c = coDestroy(this.gameObject);
            StartCoroutine(c);
        }
	}

    IEnumerator coDestroy(GameObject g)
    {
        yield return new WaitForSecondsRealtime(1);
        Destroy(g);
        yield return null;
    }

    void FixedUpdate()
	{
		float frontAngle = (Vector3.Angle(transform.up, playerTrans.position - transform.position));
		float rightAngle = (Vector3.Angle(transform.right, playerTrans.position - transform.position));
		float leftAngle = (Vector3.Angle(-transform.right, playerTrans.position - transform.position));

		if(frontAngle < maxAngle)
		{
			playerToTheRight = 0f;
		} else if(rightAngle < leftAngle)
		{
			playerToTheRight = 1.0f;
		} else
		{
			playerToTheRight = -1.0f;
		}

		//Rotating movement
		float rot = -1 * rotationSpeed * playerToTheRight * Time.deltaTime;

		transform.Rotate(0, 0, rot);

		//Straight line movement
		float l = movespeed * Time.deltaTime;
		rb.AddRelativeForce(new Vector2(0, l));

		//Make sure we can't move too fast
		if (rb.velocity.magnitude > maxSpeed)
		{
			float veloX = rb.velocity.normalized.x * maxSpeed;
			float veloY = rb.velocity.normalized.y * maxSpeed;
			rb.velocity = new Vector2(veloX, veloY);
		}
	}
}
