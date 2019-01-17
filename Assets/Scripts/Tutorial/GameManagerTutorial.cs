using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTutorial : MonoBehaviour {

    public GameObject playerObject; //Reference to the player object

    public List<GameObject> enemyList; //List of enemy objects to spawn
    public List<Transform> spawnLocations; //List of locations to spawn enemies
    public GameObject spawnParticles; //Spawn particle system to spawn with the enemies

    GameObject currEnemy = null; //Reference to the single enemy in the tutorial

    int enemyChoice = 0; //index of enemy to spawn
    int spawnChoice = 0; //index of spawn location to spawn enemy

    // Use this for initialization
    void Start () {
        setupSpawners();
    }
	
	// Update is called once per frame
	void Update () {
        if (currEnemy == null || currEnemy.transform.GetChild(0).GetComponent<AIHealth>().cur_health <= 0) //spawn only 1 enemy at a time
        {
            enemyChoice = Random.Range(0, enemyList.Count); //random spawn location
            spawnChoice = Random.Range(0, spawnLocations.Count); //random spawn location

            currEnemy = spawnEnemy(enemyList[enemyChoice], spawnLocations[spawnChoice]);
        }
	}

    //Setup all the spawners to be pointing at the target location
    void setupSpawners()
    {
        foreach (Transform t in spawnLocations)
        {
            t.LookAt(new Vector3(playerObject.transform.position.x, t.position.y, playerObject.transform.position.z));
        }
    }

    //Spawn an enemy at the given location
    GameObject spawnEnemy(GameObject enemy, Transform location)
    {
        GameObject e = Instantiate(enemy, location.position, location.rotation);
        e.transform.GetChild(0).GetComponent<AIMovement>().target = playerObject.transform;
        Instantiate(spawnParticles, location.position, location.rotation);
        return e;
    }
}
