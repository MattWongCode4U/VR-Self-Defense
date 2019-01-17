using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scenemanager : MonoBehaviour {

    //Change scene to the specified scene
    public void changeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    //Quit the game
    public void quitGame()
    {
        Application.Quit();
    }
}
