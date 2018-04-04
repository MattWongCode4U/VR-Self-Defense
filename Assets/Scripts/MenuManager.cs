using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public GameObject menuCanvas;
    public GameObject instrCanvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void activateInstruction()
    {
        instrCanvas.SetActive(true);
        menuCanvas.SetActive(false);
    }

    public void activateMenu()
    {
        menuCanvas.SetActive(true);
        instrCanvas.SetActive(false);
    }
}
