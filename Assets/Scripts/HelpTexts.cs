using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpTexts : MonoBehaviour {

    public Transform player; //player eyes location
    List<Transform> list = new List<Transform>(); //list of helpTexts

    int chosenText = 0; //index of which text to choose from

    float timelimit = 7.0f; //how long a text is displayed
    float timer = 7.0f; //track how long the text is displayed

	// Use this for initialization
	void Start () {
		foreach(Transform c in transform)
        {
            if (c.gameObject.tag == "helpText") {
                list.Add(c);
                c.gameObject.SetActive(false);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        //make the billboard texts face the player's eyes
        transform.LookAt(player.position);
        transform.Rotate(0.0f, 180.0f, 0.0f);

        //timelimit check on displaying text
        if (timer >= timelimit)
        {
            changeText();
            timer = 0.0f;
        }
        timer += Time.deltaTime;
	}

    //Cycle through the help texts randomly, not choosing the same one twice in a row
    void changeText()
    {
        int prev = chosenText;
        list[chosenText].gameObject.SetActive(false);

        while(chosenText == prev) {
            chosenText = Random.Range(0, list.Count);
        }
        list[chosenText].gameObject.SetActive(true);
    }
}
