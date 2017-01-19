using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public static int score = 0;
    private Text text;

    public void setScore(int points)
    {
        score += points;
        setText(score.ToString());
    }

    private void setText(string message)
    {
        text.text = message;
    }

    public static void reset()
    {
        score = 0;
    }
    // Use this for initialization
    void Start () {
       text = GetComponent<Text>();
        reset();
	}
	
}
