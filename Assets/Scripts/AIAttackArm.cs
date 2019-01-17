using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackArm : MonoBehaviour {

    AIMovement aimove; //Reference to AI movement script

	// Use this for initialization
	void Start () {
		aimove = transform.parent.parent.gameObject.GetComponent<AIMovement>(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "playerShield") //When the attack comes in contact with the player's shield
        {
            other.GetComponent<PlayerShield>().blocked();
            stopAttack();
        }
    }

    //Stop the attack animation
    public void stopAttack()
    {
        aimove.stopAttack();
    }
}
