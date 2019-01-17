using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public GameObject menuCanvas; //Main menu canvas reference
    public GameObject instrCanvas; //Instructions menu canvas reference

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Toggle to instructions canvas
    public void activateInstruction()
    {
        instrCanvas.SetActive(true);
        menuCanvas.SetActive(false);
    }

    //Toggle to main menu canvas
    public void activateMenu()
    {
        menuCanvas.SetActive(true);
        instrCanvas.SetActive(false);
    }
}
