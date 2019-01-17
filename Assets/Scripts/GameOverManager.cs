using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

    public GameObject scoreText; //Reference to the score text

	// Use this for initialization
	void Start () {
        scoreText.GetComponent<TextMesh>().text = "Score: " + PlayerPrefs.GetInt("Score"); //Set the text of the scoretext
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
