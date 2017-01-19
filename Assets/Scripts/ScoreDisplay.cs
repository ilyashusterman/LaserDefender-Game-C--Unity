using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    private Text text;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        text.text = Score.score.ToString();
        Score.reset();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
