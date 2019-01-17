using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManagerScript : MonoBehaviour {

    public GameObject playerObject; //Reference to the player object
    public GameObject timeDisplay; //Reference to the time display

    public List<GameObject> enemyList; //List of enemy objects to spawn
    public List<Transform> spawnLocations; //List of locations to spawn enemies
    public GameObject spawnParticles; //Spawn particle system to spawn with the enemies

    float gameTime = 0.0f; //track the time in game
    float gameTimelimit = 180.0f; //3 min, game level time limit

    float spawnTimer = 22.0f; //track time between spawning enemies
    float spawnTimelimit = 25.0f; //delay between spawning enemies

    int enemyChoice = 0; //index of enemy to spawn
    int spawnChoice = 0; //index of spawn location to spawn enemy

    ArrayList spawnList = new ArrayList();

    bool firstPass = true;

    // Use this for initialization
    void Start () {
        PlayerPrefs.SetInt("Score", 0); //ensure score is reset

        setupSpawners();
	}
	
	// Update is called once per frame
	void Update () {
        gameTime += Time.deltaTime;

        updateTimeDisplay();

        if (gameTime >= gameTimelimit) //reached the time limit
        {
            //end game
            saveScore();
            SceneManager.LoadScene("GameOver");
        }

        //check if time to spawn a new enemy
        if (spawnTimer >= spawnTimelimit)
        {
            enemyChoice = Random.Range(0, enemyList.Count); //random spawn location

            if(spawnList.Count == spawnLocations.Count)
            {
                spawnList.Clear();
            }
            spawnChoice = Random.Range(0, spawnLocations.Count); //random spawn location
            while (spawnList.Contains(spawnChoice)) //ensure that different spawns are being used
            {
                spawnChoice = Random.Range(0, spawnLocations.Count);
            }
            spawnList.Add(spawnChoice);

            //spawn the enemy
            spawnEnemy(enemyList[enemyChoice], spawnLocations[spawnChoice]);
            spawnTimer = 0.0f;
        } else
        {
            spawnTimer += Time.deltaTime;
        }
	}

    //Return the time remaining for the game
    float getTimeRemaining()
    {
        return gameTimelimit - gameTime;
    }

    //Update the time text with the amound of time remaining formatted to 2 decimal places
    void updateTimeDisplay()
    {
        timeDisplay.GetComponent<TextMesh>().text = getTimeRemaining().ToString("F2") + " Seconds";
    }

    //Setup all the spawners to be pointing at the target location
    void setupSpawners()
    {
        foreach(Transform t in spawnLocations)
        {
            t.LookAt(new Vector3(playerObject.transform.position.x, t.position.y, playerObject.transform.position.z));
        }
    }

    //Spawn an enemy at the given location
    void spawnEnemy(GameObject enemy, Transform location)
    {
        GameObject e = Instantiate(enemy, location.position, location.rotation);
        e.transform.GetChild(0).GetComponent<AIMovement>().target = playerObject.transform;
        Instantiate(spawnParticles, location.position, location.rotation);
    }

    //Save score locally
    public void saveScore()
    {
        string fileName = Application.persistentDataPath + "/Highscores.dat";
        int score = PlayerPrefs.GetInt("Score");

        List<ScoreData> list;
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file;
        if (File.Exists(fileName))
        {
            file = File.OpenRead(fileName);
            list = (List<ScoreData>)bf.Deserialize(file);
            file.Close();
            File.Delete(fileName);
        } else
        {
            list = new List<ScoreData>();
        }

        ScoreData newdata = new ScoreData(score); //new data to save

        if(list.Count >= 5) //too many highscores
        {
            if(newdata.score > list[0].score)
            {
                list.RemoveAt(0);
                list.Add(newdata);
            }
        } else //room for more highscores
        {
            list.Add(newdata);
        }

        list.Sort((x, y) => x.score.CompareTo(y.score)); //sort the list in descending order
        Debug.Log("new score: " + newdata.score);
        Debug.Log("saving scores");
        foreach(ScoreData s in list)
        {
            Debug.Log(s.score);
        }

        file = File.Create(fileName);

        bf.Serialize(file, list);
        file.Close();
    }
}

//Info of 1 highscore entry
[System.Serializable]
public class ScoreData
{
    public int score;

    public ScoreData(int _score)
    {
        score = _score;
    }
}