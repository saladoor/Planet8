using UnityEngine;
using System.Collections;

public class SlowMoController : MonoBehaviour {
    
    public GameObject player;
    private int threatsNearby = 0;
	private float slowDown = 0.3f;

	private float planeRot;
	private float planeSpeed;
	private PlaneController pc;

	void Start()
	{
		pc = player.GetComponent<PlaneController>();
		planeRot = pc.rotationSpeed;
		planeSpeed = pc.movespeed;
	}

    void FixedUpdate()
    {
        transform.position = player.GetComponent<Transform>().position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Threat")
        {
            threatsNearby++;
            Time.timeScale = slowDown;
			pc.rotationSpeed = planeRot * (1 / slowDown);
			pc.movespeed = planeSpeed * (1 / slowDown)*10f; //10 is a random feel-good number
            //playerSpeed = 0.05F * Time.timeScale;
            //player.GetComponent<BulletPlayerController>().rotationSpeed = 400.0F;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Threat")
        {
            threatsNearby--;
            if (threatsNearby < 1)
            {
                Time.timeScale = 1.0f;
				pc.rotationSpeed = planeRot;
				pc.movespeed = planeSpeed;
				//playerSpeed = 0.05F;
				//player.GetComponent<BulletPlayerController>().rotationSpeed = 200.0F;
				Time.fixedDeltaTime = 0.02F;
			}
        }
    }
}
