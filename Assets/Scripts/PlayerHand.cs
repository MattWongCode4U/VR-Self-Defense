using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {

    public float speed; //Speed at which the arm is moving
    public GameObject armParent; //Reference to parent object

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Calculate damage from the GameArms script speed
    public int getDamage()
    {
        armParent.GetComponent<GameArms>().hapticPulse(); //controller haptic feedback when hitting
        armParent.GetComponent<GameArms>().playHitSound(); //audio feedback when hitting

        int damage = (int)speed * 10;
        if (damage < 5)
        {
            damage = 5; //min damage
        }
        if (damage > 50)
        {
            damage = 50; //max damage
        }
        return damage;
    }
}
