using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public GameObject healthBar; //Health bar canvas reference
    public float max_health = 100f; //Max health of player
    public float cur_health = 100f; //Current health of player

    // Use this for initialization
    void Start () {
        cur_health = max_health;
    }
	
	// Update is called once per frame
	void Update () {

    }

    //Take damage and check if still alive
    public void takeDamage(float damage)
    {
        int score = PlayerPrefs.GetInt("Score");
        score -= 20;
        PlayerPrefs.SetInt("Score", score);
        cur_health -= damage;
        Debug.Log("Score: " + PlayerPrefs.GetInt("Score"));
        updateHealthBar(cur_health / max_health);
        if (cur_health <= 0)
        {
            die();
        }
    }

    //When health reaches 0, destroy this gameObject
    void die()
    {
        GameObject.Find("Game Manager").GetComponent<GameManagerScript>().saveScore();
        SceneManager.LoadScene("GameOver");
        Destroy(gameObject);
    }

    //Update the health bar
    void updateHealthBar(float healthPercent)
    {
        healthBar.transform.localScale = new Vector3(healthPercent, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
