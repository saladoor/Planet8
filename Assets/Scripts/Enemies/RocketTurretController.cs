using UnityEngine;
using System.Collections;

public class RocketTurretController : MonoBehaviour
{
    public GameObject b; //Rocekt object
    public Transform player; //Transform of player

    private RocketController rc;
    private int refireCounter = 0;
    private int refireTimer = 180;
    public float rad = 0.8f;

    // Use this for initialization
    void Start()
    {
        //Fire off a rocket
        fire();
    }

    void Update()
    {
        if (rc.dead) //If old rocket is dead, fire new one
        {
            refireCounter++;
            if(refireCounter > refireTimer)
            {
                fire();
                refireCounter = 0;
            }
        }
    }

    private void fire()
    {
		//Sounds!
		GetComponent<AudioSource>().Play();

		//Fire off a rocket
		Vector2 spawnPos = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), player.position, rad);
		float frontAngle = (Vector3.Angle(transform.up, player.transform.position - transform.position)); //Angle between player and turret
		float rightAngle = (Vector3.Angle(transform.right, player.transform.position - transform.position));
		float leftAngle = (Vector3.Angle(-transform.right, player.transform.position - transform.position));

		if (rightAngle < leftAngle)
		{
			frontAngle = -frontAngle;
		}

		Object inst = Instantiate(b, spawnPos, Quaternion.AngleAxis(frontAngle, transform.forward /*Quaternion.identity*/));
        GameObject rocket = inst as GameObject;

        //Save this rocket
        rc = rocket.GetComponent<RocketController>();
        rc.playerTrans = player;
    }
}
