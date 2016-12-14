using UnityEngine;
using System.Collections;

public class SlowMoController : MonoBehaviour {
    
    public GameObject player;
	public float slowDown = 0.3f;

	private int threatsNearby = 0;
	private PlaneController pc;

	void Start()
	{
		pc = player.GetComponent<PlaneController>();
		pc.slowdown = slowDown;
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
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
			pc.slowmoStart = true;
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
				Time.fixedDeltaTime = 0.02F;
				pc.slowmoEnd = true;
			}
		}
	}
}
