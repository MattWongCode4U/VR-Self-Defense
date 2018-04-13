using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerData
{
    int[] hiScores;

    public PlayerData(int numHighScores)
    {
        hiScores = new int[numHighScores];
    }

    public void SetScore(int i, int score)
    {
        hiScores[i] = score;
    }

    public int GetScore(int i)
    {
        return hiScores[i];
    }

    public void Save(string path)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);

        bf.Serialize(file, this);
        file.Close();
    }

    public static PlayerData Load(string path)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);

        PlayerData data = (PlayerData)bf.Deserialize(file);
        file.Close();
        return data;
    }
}

public class Scenemanager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Testing saving and loading highscores
        /*
        PlayerData pData = new PlayerData(10);

        for(int i = 0; i < 10; i++)
        {
            pData.SetScore(i, i * 10);
        }

        pData.Save(Application.persistentDataPath + "/HighScores.dat");

        PlayerData pData2 = PlayerData.Load(Application.persistentDataPath + "/HighScores.dat");

        for(int i = 0; i < 10; i++)
        {
            Debug.Log(pData2.GetScore(i));
        }
        */
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
