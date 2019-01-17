using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour {

    public GameObject healthBar; //Healthbar reference
    public float max_health = 100f; //Max health of the AI
    public float cur_health = 0f; //Current health of the AI

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
        cur_health -= damage;
        updateHealthBar(cur_health / max_health);
        if(cur_health <= 0)
        {
            die();
        }
    }

    //When health reaches 0, destroy this gameObject
    void die()
    {
        int score = PlayerPrefs.GetInt("Score");
        score += 100;
        PlayerPrefs.SetInt("Score", score);
        Debug.Log("Score: " + PlayerPrefs.GetInt("Score"));
        Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }

    //Update the health bar
    void updateHealthBar(float healthPercent)
    {
        healthBar.transform.localScale = new Vector3(healthPercent, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
