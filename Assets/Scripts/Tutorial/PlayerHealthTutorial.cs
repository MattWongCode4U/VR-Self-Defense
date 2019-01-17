using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthTutorial : MonoBehaviour {

    public GameObject healthBar; //Health bar canvas reference
    public float max_health = 100f; //Max health of player
    public float cur_health = 100f; //Current health of player

    //Variables for reseting the health of the player, tutorial purposes only
    bool tookDamage = false; 
    float timelimit = 3.0f;
    float timer = 0.0f;

    // Use this for initialization
    void Start () {
        cur_health = max_health;
    }
	
	// Update is called once per frame
	void Update () {
        if (tookDamage)
        {
            timer += Time.deltaTime;
        }

        if(timer >= timelimit)
        {
            restoreHealth();
        }
	}

    //Take damage and check if still alive
    public void takeDamage(float damage)
    {
        cur_health -= damage;
        updateHealthBar(cur_health / max_health);
        tookDamage = true;
    }

    //Reset health, tutorial only
    public void restoreHealth()
    {
        cur_health = max_health;
        updateHealthBar(cur_health / max_health);
        timer = 0.0f;
        tookDamage = false;
    }

    //Update the health bar
    void updateHealthBar(float healthPercent)
    {
        healthBar.transform.localScale = new Vector3(healthPercent, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
