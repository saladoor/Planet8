using UnityEngine;
using System.Collections;

public class RocketTurretController : MonoBehaviour
{
    public GameObject b; //Rocekt object
    public Transform player; //Transform of player

    private RocketController rc;
    private int refireCounter = 0;
    private int refireTimer = 180;
    public float rad = 1.2f;

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
        //Fire off a rocket
        Vector2 spawnPos = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y - 0.64f), player.position, rad);
        Object inst = Instantiate(b, spawnPos, Quaternion.identity);
        GameObject rocket = inst as GameObject;

        //Save this rocket
        rc = rocket.GetComponent<RocketController>();
        //rc.transform.rotation.eulerAngles = new Vector3(0, 0, 0);
        rc.playerTrans = player;
    }
}
