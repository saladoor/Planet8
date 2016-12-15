using UnityEngine;
using System.Collections;

public class backgroundSoundController : MonoBehaviour {
    public PlaneController player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(player.GetComponent<PlayerHealth>().currentHealth < 0)
        {
            GetComponent<AudioSource>().volume = 0;
        }
		GetComponent<AudioSource>().pitch = Time.timeScale;
	}
}
