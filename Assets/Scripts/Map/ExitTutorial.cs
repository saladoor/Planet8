﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitTutorial : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
		if(col.tag == "Player")
		{
			SceneManager.LoadScene(1); //this should be the index of the scene in the build settings
		}
    }
}
