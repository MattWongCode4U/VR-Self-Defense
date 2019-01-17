using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LoadHighScore : MonoBehaviour {

    List<Transform> textlist = new List<Transform>(); //List of text object that will hold the scores
    List<ScoreData> scores; //List of the actual score data
    public GameObject checkText; //Text if there isn't any score data

	// Use this for initialization
	void Start () {
        //get all children highscore texts
        foreach(Transform child in transform)
        {
            textlist.Add(child);
        }

        scores = loadScore();
        updateHighscoreTexts();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Update highscore texts fields
    void updateHighscoreTexts()
    {
        //clear texts
        foreach (Transform c in textlist)
        {
            c.gameObject.GetComponent<TextMesh>().text = "";
        }
        checkText.GetComponent<TextMesh>().text = "";

        //set texts
        if (scores != null)
        {
            for(int i = 0, j = scores.Count - 1; i < scores.Count; i++, j--)
            {
                textlist[i].gameObject.GetComponent<TextMesh>().text = "Score: " + scores[j].score.ToString();
            }
        } else
        {
            checkText.GetComponent<TextMesh>().text = "No Data Found";
        }
    }

    //Load scores from local file
    public List<ScoreData> loadScore()
    {
        string fileName = Application.persistentDataPath + "/Highscores.dat";
        FileStream file;

        if (File.Exists(fileName))
        {
            file = File.OpenRead(fileName);
        }
        else
        {
            Debug.LogError("File not found");
            return null;
        }

        BinaryFormatter bf = new BinaryFormatter();
        List<ScoreData> list = (List<ScoreData>)bf.Deserialize(file); //Load list from file
        file.Close();
        return list;
    }

    //delete entirety of highscores data
    public void deleteScores()
    {
        string fileName = Application.persistentDataPath + @"/Highscores.dat";
        File.Delete(fileName); //delete file to refresh the highscores
    }
}
