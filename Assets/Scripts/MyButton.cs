using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyButton : MonoBehaviour {

    public enum ButtonType { MainMenu, Instructions, InstructionsReturn, Highscore, Start, Quit, StageSelect, Tutorial}; //Button enums for all the different button types
    public ButtonType type;
    ButtonType buttonType;

	// Use this for initialization
	void Start () {
        buttonType = type;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Handle when the button is typed
    public void Clicked()
    {
        if (buttonType == ButtonType.MainMenu)
        {
            changeScene("StartMenu");
        } else if (buttonType == ButtonType.Instructions)
        {
            GetComponent<MenuManager>().activateInstruction();
        } else if (buttonType == ButtonType.InstructionsReturn)
        {
            GetComponent<MenuManager>().activateMenu();
        } else if (buttonType == ButtonType.Highscore)
        {
            changeScene("HighScore");
        } else if (buttonType == ButtonType.Start)
        {
            changeScene("Main");
        } else if (buttonType == ButtonType.Quit)
        {
            quitGame();
        } else if (buttonType == ButtonType.StageSelect) {
            changeScene("StageSelect");
        } else if (buttonType == ButtonType.Tutorial)
        {
            changeScene("Tutorial");
        }
    }

    //Change to specified scene
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
