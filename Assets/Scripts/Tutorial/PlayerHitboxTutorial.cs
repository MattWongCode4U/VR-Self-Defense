using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitboxTutorial : MonoBehaviour {
    public bool playHitSound; //Flag for playing the hit sound, currently only for getting hit on the head
    public int damage; //Damage taken from a hit
    GameObject player; //Reference to player object

    // Use this for initialization
    void Start()
    {
        player = transform.parent.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "aiAttack") //Hit by ai attack
        {
            //Player is hit
            Debug.Log("col " + other.gameObject.name);
            if (playHitSound)
            {
                GetComponent<AudioSource>().Play(); //play hit sound
            }
            player.GetComponent<PlayerHealthTutorial>().takeDamage(damage);
            other.gameObject.GetComponent<AIAttackArm>().stopAttack();
        }
    }

}
