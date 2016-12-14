using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScore : MonoBehaviour
{
    public static int mineScore = 5;
    public static int bulletScore = 10;
    public static int rocketScore = 20;

    public static int score;        // The player's score.


    Text text;                      // Reference to the Text component.


    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();

        // Reset the score.
        score = 0;
    }


    void Update()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        text.text = "Score: " + score;
    }
}