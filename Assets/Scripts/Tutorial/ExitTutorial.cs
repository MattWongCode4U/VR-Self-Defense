using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTutorial : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        //Vital Point is hit
        if (other.gameObject.tag == "playerAttack")
        {
            SceneManager.LoadScene("StartMenu");
        }
    }
}
