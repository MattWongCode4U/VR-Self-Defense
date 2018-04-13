using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour {

    public GameObject healthBar;
    public float max_health = 100f;
    public float cur_health = 0f;

	// Use this for initialization
	void Start () {
        cur_health = max_health;
	}
	
	// Update is called once per frame
	void Update () {
        //taking damage test
        /*if (Input.GetKeyDown("f"))
        {
            takeDamage(11.0f);
        }*/
	}

    //Take damage and check if still alive
    void takeDamage(float damage)
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
        Destroy(gameObject);
    }

    //Update the health bar
    void updateHealthBar(float healthPercent)
    {
        healthBar.transform.localScale = new Vector3(healthPercent, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
